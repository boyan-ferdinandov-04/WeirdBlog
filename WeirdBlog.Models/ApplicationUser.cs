
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WeirdBlog.Utility;

namespace WeirdBlog.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [StringLength(StaticConstants.UserNameMaxLength,ErrorMessage = "Username cannot be longer that 50 characters!")]
        public string UserName { get; set; }
    }
}
