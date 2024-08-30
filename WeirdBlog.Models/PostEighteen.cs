using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WeirdBlog.Utility;

namespace WeirdBlog.Models
{
    public class PostEighteen
    {
        [Key]
        public Guid PostEighteenId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(StaticConstants.PostTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int CategoryEighteenId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public CategoryEighteen CategoryEighteen { get; set; }
    }
}
