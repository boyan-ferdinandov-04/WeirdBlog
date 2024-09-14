
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeirdBlog.Utility;

namespace WeirdBlog.Models
{
    [NotMapped]
    public class ApplicationUser:IdentityUser<Guid>
    {
        [Required]
        [StringLength(StaticConstants.UserNameMaxLength,ErrorMessage = "Username cannot be longer that 50 characters!")]
        public string UserName { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
