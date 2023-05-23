using CasanovaExchange.Models;
using Microsoft.AspNetCore.Identity;

namespace CasanovaExchange.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> userManager;
        public int? CurrentUserId { get; set; }

        public UserRepository(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> CreateUser(SignUpModel signupmodel)
        {
            var user = new IdentityUser()
            {
                Email = signupmodel.Email,
                UserName = signupmodel.Email
            };

            var result = await userManager.CreateAsync(user, signupmodel.Password);
            return result;
        }
    }
}
