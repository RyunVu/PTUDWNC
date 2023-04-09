using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebWaterPaintStore.Services.WaterPaints;
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

            return app;
        }

        private static async Task<IResult> Login(
        [FromBody] UserLogin userLogin,
        [FromServices] IUserRepository userRepo,
        [FromServices] IConfiguration configuration,
        [FromServices] IMapper mapper)
        {
            var user = Authenticate(userLogin, userRepo, mapper);

            UserDto userDto = await user;
            if (userDto != null)
            {
                var token = Generate(userDto, configuration);

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


        private static UserDto GetCurrentUser(
            HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserDto
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Name = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                    Id = int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Sid)?.Value),
                };
            }
            return null;
        }

        private static JwtSecurityToken Generate(
            UserDto user,
            IConfiguration config)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
        };

            //foreach (var role in listRoles)
            //{
            //	Array.Resize(ref claims, claims.Length + 1);
            //	claims[claims.Length - 1] = new Claim("role", role);
            //}

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(15),
                signingCredentials: credentials);

            return token;
        }

        private static async Task<UserDto> Authenticate(UserLogin userLogin, IUserRepository userRepoo, IMapper mapper)
        {
            var currentUser = await userRepoo.GetUser(userLogin.Username, userLogin.Password);
            var result = mapper.Map<UserDto>(currentUser);
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
