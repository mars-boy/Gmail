using AuthenticationService.Entities;
using AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Repositories
{
    public interface IAuthRepository
    {

        Task<User> Login(LoginModel loginModel);
    }
}
