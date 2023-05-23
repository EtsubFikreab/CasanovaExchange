using CasanovaExchange.Repository;
using Microsoft.AspNetCore.Mvc;
using CasanovaExchange.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System;
namespace CasanovaExchange.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly IUserRepository iuserrepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,IUserRepository iuserRepository)
        {
            this.iuserrepository=iuserRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
       
        public  async Task<IActionResult> signin(SigninModel signInModel) {

          
            if (!ModelState.IsValid)
                return View(signInModel);

            var user = await userManager.FindByEmailAsync(signInModel.Email);
            if (user != null)
            {
                //user is found check password
                var passwordcheck = await userManager.CheckPasswordAsync(user, signInModel.Password);
                if (passwordcheck)
                {
                   // password correct signing in
                    var result = await signInManager.PasswordSignInAsync(user, signInModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                // password is incorrect

                TempData["Error"] = "wrong credentials.please try again";

                return View(signInModel);
            }
            //user not found

            TempData["Error"] = "wrong credentails. please try again";
            return View(signInModel);  
        }

      

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel signupModel)
        {
            if (!ModelState.IsValid)
                return View(signupModel);
            var user = await userManager.FindByEmailAsync(signupModel.Email);
            if (user != null)
            {
                TempData["Error"] = "This Email Address is already in Use";
                return View(signupModel);
            }

            var newUser = new IdentityUser
            {
                UserName = signupModel.Email,
                Email = signupModel.Email
            };

            var result = await userManager.CreateAsync(newUser, signupModel.Password);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            return View(signupModel);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
