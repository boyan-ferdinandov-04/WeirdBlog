using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeirdBlog.Utility;

namespace WeirdBlog.Models
{
    public class Post
    {
        [Key]
        public Guid PostId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(StaticConstants.PostTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; } 

        public string ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [ValidateNever]
        public ApplicationUser User { get; set; }
    }
}
