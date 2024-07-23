using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Data.Repository.IServices
{
   public interface IAccountService
    {
        Task<(IdentityResult res,IdentityUser user)> CreateUserAsync(SignUpViewModel model);
        Task<SignInResult> LoginUserAsync(LoginViewModel model);
        Task LogoutUserAsync();
    }
}
