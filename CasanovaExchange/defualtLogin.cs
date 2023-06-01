using Microsoft.AspNetCore.Identity;

namespace CasanovaExchange
{
    public class defualtLogin
    {


        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> rolemanager;
        private readonly IServiceProvider serviceProvider;


        public defualtLogin() { }
        public defualtLogin(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityUser> roleManager)
        {

            this.serviceProvider = serviceProvider;
            this.userManager = userManager;
            this.signInManager = signInManager;
            rolemanager = rolemanager;

        }

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

    }
}
