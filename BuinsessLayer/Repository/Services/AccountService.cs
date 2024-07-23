using BuisnessLayer.Data.Repository.IServices;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Data.Repository.Services
{
 
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountService(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<(IdentityResult res,IdentityUser user)> CreateUserAsync(SignUpViewModel model)
        {
            var user = new IdentityUser()
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await userManager.CreateAsync(user, model.Password);
            return (result, user);
        }

        public async Task<SignInResult> LoginUserAsync(LoginViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Name,
                 model.Password,
                 model.RememberMe, false);
            return result;
        }

        public async Task LogoutUserAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
