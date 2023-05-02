using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using WebWaterPaintStore.Core.Entities;
using WebWaterPaintStore.Core.Identity;
using WebWaterPaintStore.Services.WaterPaints;
using WebWaterPaintStore.WebApi.Identities;
using WebWaterPaintStore.WebApi.Models;

namespace WebWaterPaintStore.WebApi.Endpoints
{
    public static class AccountEndPoints{

        public static WebApplication MapAccountEndPoints(this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/account");

            routeGroupBuilder.MapPost("/", Login)
            .WithName("Login")
            .AllowAnonymous();

            routeGroupBuilder.MapPost("/Register", Register)
                .WithName("Register");

            routeGroupBuilder.MapGet("/GetProfile", GetProfile)
                .WithName("GetProfile")
                .RequireAuthorization()
                .Produces<ApiResponse<UserDto>>();

            return app;
        }

        private static async Task<IResult> Login(
        [FromBody] UserLogin userLogin,
        [FromServices] IUserRepository userRepo,
        [FromServices] IConfiguration configuration,
        [FromServices] IMapper mapper)
        {
            var user = IdentityManager.Authenticate(userLogin, userRepo, mapper);

            UserDto userDto = await user;
            if (userDto != null)
            {
                var token = IdentityManager.Generate(userDto, configuration);

                var accessToken = new AccessTokenModel()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    UserDto = userDto,
                    ExpiresToken = token.ValidTo,
                    TokenType = "bearer"
                };

                return Results.Ok(accessToken);
            }

            return Results.NotFound("User not found");
        }

        private static async Task<IResult> Register(
            [FromBody] UserEditModel model,
            [FromServices] IUserRepository userRepo,
            [FromServices] IConfiguration configuration,
            [FromServices] IMapper mapper)
        {
            var user = mapper.Map<User>(model);

            var userExist = await userRepo.IsUserExistedAsync(user.Username);
            if (userExist)
            {
                return Results.NotFound("Account is existed");
            }
            var newUser = await userRepo.Register(user, model.ListRoles);

            var userDto = mapper.Map<UserDto>(newUser);

            return Results.Ok(userDto);
        }

        private static async Task<IResult> GetProfile(
            HttpContext context,
            [FromServices] IUserRepository userRepo,
            [FromServices] IMapper mapper)
        {
            try
            {
                var identity = IdentityManager.GetCurrentUser(context);
                var user = await userRepo.GetUserByIdAsync(identity.Id);
                var userDto = mapper.Map<UserDto>(user);

                return Results.Ok(ApiResponse.Success(userDto));
            }
            catch (Exception e)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, e.Message));
            }
        }


    }
}
