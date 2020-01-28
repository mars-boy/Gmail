using AuthenticationService.Entities;
using AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        public async Task<User> Login(LoginModel loginModel)
        {
            var userDetails = new User() {
                Id = Guid.NewGuid(),
                UserName = "AARON"
            };
            return userDetails;
        }
    }
}
