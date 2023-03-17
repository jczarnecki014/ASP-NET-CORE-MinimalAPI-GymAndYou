using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace GymAndYou___MinimalAPI___Project
{
    public static class AccountEndpoints
    {
        public static WebApplication RegisterAccountEndpoints(this WebApplication app)
        {
            app.MapGet("/token", (AuthenticationDetails details) =>
            {
                var jwtIssuer = details.JwtIssuer;
                var jwtAudience = details.JwtIssuer;
                var jwtExpire = DateTime.Now.AddDays(details.JwtExpireDays);
                var jwtKey = details.JwtKey;

                Claim[] claims = new Claim[] 
                {
                    new Claim(ClaimTypes.NameIdentifier,new Guid().ToString()),
                    new Claim(ClaimTypes.Name,"DefaultUser"),
                    new Claim(ClaimTypes.Role,"FullAccess")
                };

                SymmetricSecurityKey symetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                SigningCredentials credentials = new SigningCredentials(symetricKey,SecurityAlgorithms.HmacSha256);

                JwtSecurityToken token = new JwtSecurityToken
                (
                    jwtIssuer,
                    jwtAudience,
                    claims,
                    expires: jwtExpire,
                    signingCredentials: credentials
                );

                var useFullToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Results.Ok(useFullToken);
            });

            return app;
        }
    }
}
