﻿using CasanovaExchange.Repository;
using Microsoft.AspNetCore.Mvc;
using CasanovaExchange.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.CodeAnalysis.Differencing;
using NuGet.Protocol;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;

namespace CasanovaExchange.Controllers
{

	public class AccountController : Controller
	{
		private readonly IUserRepository iuserrepository;
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly RoleManager<IdentityRole> rolemanager;
		// private readonly IHttpContextAccessor httpContextAccessor;
		private readonly IServiceProvider serviceProvider;
		private readonly ICommodityRepository _commodityRepository;
		public AccountController(UserManager<IdentityUser> userManager, IServiceProvider serviceProvider
, SignInManager<IdentityUser> signInManager, IUserRepository iuserRepository, RoleManager<IdentityRole> roleManager, ICommodityRepository commodityRepository)
		{
			this.iuserrepository = iuserRepository;
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.rolemanager = rolemanager;
			this.serviceProvider = serviceProvider;
			_commodityRepository = commodityRepository;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Signin()
		{
			return View();
		}
		[HttpPost]
		[AllowAnonymous]


        public async Task defualt()
        {
            string email = "admin@gmail.com";
            var password = "123ADMIN_admin";

            //await signInManager.SignOutAsync();
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var newUser = new IdentityUser
                {
                    UserName = email,
                    Email = email,

                };

                var result = await userManager.CreateAsync(newUser, password);

                var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roleExist = await RoleManager.RoleExistsAsync("Admin");

                if (!roleExist)
                {
                    await RoleManager.CreateAsync(new IdentityRole("Admin"));
                }
                await userManager.AddToRoleAsync(newUser, "Admin");
            }

        }

        public async Task<IActionResult> signin(SigninModel signInModel)
		{
            
         await   defualt();

            if (!ModelState.IsValid)
				return View(signInModel);

			var user = await userManager.FindByEmailAsync(signInModel.Email);
			if (user != null)
			{
				//user is found check password
				var passwordcheck = await userManager.CheckPasswordAsync(user, signInModel.Password);
				if (passwordcheck)
				{
				//	var checkRole= userManager.IsInRoleAsync();
                    // password correct signing in
                    var result = await signInManager.PasswordSignInAsync(user, signInModel.Password, false, false);
					if (result.Succeeded)
					{
					//	if(checkRole)
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
			{
				//checks if that Role exists if it doesn't it creates it

				var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				var roleExist = await RoleManager.RoleExistsAsync("User");
				if (!roleExist)
				{
					await RoleManager.CreateAsync(new IdentityRole("User"));
				}

				await userManager.AddToRoleAsync(newUser, "User");
				TempData["message"] = "register successfull";
				return RedirectToAction("Index", "Home");
			}
			return View(signupModel);
		}
		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		// a function that randomlt selects the profile picture
		public string GetRandomImage(string folderPath)
		{
			// Get all the image files in the folder
			var extensions = new string[] { ".png", ".jpg", ".gif" };
			var files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
								 .Where(f => extensions.Contains(Path.GetExtension(f).ToLower()));

			// Check if there are any files
			if (files.Any())
			{
				// Create a random number generator
				var random = new Random();

				// Pick a random file
				var file = files.ElementAt(random.Next(files.Count()));

				// Return the file path
				return file;
			}
			else
			{
				// Return null if no files are found
				return null;
			}
		}

		// after this is the seeting where u can edit users info while checking if that username email are unique


		[Authorize]
		public IActionResult Setting()
		{
			return View();
		}

		[HttpGet]
		[Authorize]

		public async Task<IActionResult> Settings()
		{
			var user = await userManager.GetUserAsync(User);

			if (user == null)
			{
				return View("Error");
			}

			var settingMV = new settingModel()
			{
				UserName = user.UserName,
				Email = user.Email,
				Password = user.PasswordHash,
				phoneNumber = user.PhoneNumber,
			};
			return View(settingMV);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Setting(settingModel settingMV)
		{


			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Failed to edit profile");
				return View("Setting", settingMV);
			}
			var user = await userManager.GetUserAsync(User);
			if (User == null)
			{
				return View("Error");
			}

			if (settingMV.UserName != null)
			{
				var usercheck = await userManager.FindByNameAsync(settingMV.UserName);
				if (usercheck != null && user.UserName == settingMV.UserName)
				{
					TempData["Errors"] = "This username is already in Use";
					return View(settingMV);
				}
				user.UserName = settingMV.UserName;
			}
			if (settingMV.Email != null && user.Email == settingMV.Email)
			{
				var usercheck = await userManager.FindByEmailAsync(settingMV.Email);
				if (usercheck != null && user.Email == settingMV.Email)
				{
					TempData["Error"] = "This Email Address is already in Use";
					return View(settingMV);
				}
				user.Email = settingMV.Email;

			}
			if (settingMV.Password != null)
			{

				userManager.ChangePasswordAsync(user, user.PasswordHash, settingMV.Password);
			}
			if (settingMV.phoneNumber != null)
			{
				user.PhoneNumber = settingMV.phoneNumber;
			}

			await userManager.UpdateAsync(user);

			TempData["Message"] = "Change successfull !!";


			return RedirectToAction("setting", "Account");
		}
	}
}
