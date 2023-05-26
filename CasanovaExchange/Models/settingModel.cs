using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;

namespace CasanovaExchange.Models
{
    public class settingModel
    {

        [EmailAddress]
        public string? Email { get; set; }
     
        public string? Password { get; set; }
        
        public string? UserName { get; set; }
        [Phone]
        public string? phoneNumber { get; set; }
      
    }
}
