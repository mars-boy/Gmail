using AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Services
{
    public interface IAuthService
    {

        Task<UserDetails> Login(LoginModel loginModel);

        Task<UserDetails> RefreshToken(UserDetails userDetails);
    }
}
