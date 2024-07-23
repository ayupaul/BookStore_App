using BuisnessLayer.Data.Repository.IServices;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService,SignInManager<IdentityUser> signInManager,ILoggerFactory loggerFactory)
        {
            this.accountService = accountService;
            this.signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }



        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            var validator = new SignUpViewModelValidator();
            var res = validator.Validate(signUpViewModel);
            if (!res.IsValid)
            {
                return BadRequest(res.Errors);
            }
            if (ModelState.IsValid)
            {
                var (result,user) = await accountService.CreateUserAsync(signUpViewModel);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"UserName with {signUpViewModel.UserName} has Successfully SignedUp");
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }



                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }



            return View(signUpViewModel);



        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await accountService.LoginUserAsync(loginViewModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }



                ModelState.AddModelError(string.Empty, "Invalid Login Input");
            }



            return View(loginViewModel);



        }



        [HttpPost]
        public async Task<IActionResult> logout()
        {
            await accountService.LogoutUserAsync();
            return RedirectToAction("index", "home");
        }
    }
}

