using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using WebWaterPaintStore.Core.Entities;
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
      
    }
}
