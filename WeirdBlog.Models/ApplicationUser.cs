
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeirdBlog.Utility;

namespace WeirdBlog.Models
{
    [NotMapped]
    public class ApplicationUser:IdentityUser<Guid>
    {
        [StringLength(StaticConstants.DescriptionMaxLength)]
        public string Description { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
