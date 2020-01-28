using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService.Models;
using AuthenticationService.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationService.Services
{
    public class AuthService : IAuthService
    {

        private AuthServiceConfig _authServiceConfig;
        private IAuthRepository _authRepository;

        public AuthService(IOptionsSnapshot<AuthServiceConfig> authServiceConfig,
            IAuthRepository authRepository) {
            this._authServiceConfig = authServiceConfig.Value;
            this._authRepository = authRepository;
        }


        public async Task<UserDetails> Login(LoginModel loginModel)
        {
            UserDetails userDetails = null;
            var user = await _authRepository.Login(loginModel);
            if (user != null) {
                userDetails = new UserDetails()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                userDetails.Token = await AddToken(userDetails);
            }
            return userDetails;
        }

        public async Task<UserDetails> RefreshToken(UserDetails userDetails)
        {
            UserDetails modifiedUserDetails = null;
            modifiedUserDetails = await RefreshUserDetails(userDetails);
            return userDetails;
        }

        #region PrivateMethods

        public async Task<string> AddToken(UserDetails userDetails) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authServiceConfig.Secret);
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = DateTime.Now.ToUniversalTime() - origin;
            double se = Math.Floor(diff.TotalSeconds);
            var timeInTicks = (long)se * TimeSpan.TicksPerSecond;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userDetails.UserId.ToString()),
                    new Claim("Role", "Admin")
                }),
                Expires = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                    .AddTicks(timeInTicks).AddMinutes(_authServiceConfig.TokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userDetails.Token = tokenHandler.WriteToken(token);
            return userDetails.Token;
        }

        public async Task<UserDetails> RefreshUserDetails(UserDetails userDetails) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authServiceConfig.Secret);
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = DateTime.Now.ToUniversalTime() - origin;
            double se = Math.Floor(diff.TotalSeconds);
            var timeInTicks = (long)se * TimeSpan.TicksPerSecond;

            var tokenValidationParams = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authServiceConfig.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = null;
            var principal = jwtHandler.ValidateToken(userDetails.Token,
                    tokenValidationParams, out var validatedToken);
            jwtToken = validatedToken as JwtSecurityToken;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(jwtToken.Claims),
                Expires = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                    .AddTicks(timeInTicks).AddMinutes(_authServiceConfig.TokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userDetails.Token = tokenHandler.WriteToken(token);
            return userDetails;
        }

        #endregion PrivateMethods
    }
}
