
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeirdBlog.Utility;

namespace WeirdBlog.Models
{
    [NotMapped]
    public class ApplicationUser:IdentityUser<Guid>
    {
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
