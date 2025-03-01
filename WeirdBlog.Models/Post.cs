using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
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

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsApproved { get; set; } = false;

        [ValidateNever]
        public string Slug { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [ValidateNever]
        public ApplicationUser User { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<Like> Likes { get; set; } = new List<Like>();
        
    }
}
