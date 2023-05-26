using CasanovaExchange.Models;
using Microsoft.AspNetCore.Identity;

namespace CasanovaExchange.Repository
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUser(SignUpModel signupmodel);
        
       
    }
}
