using System.ComponentModel.DataAnnotations;
using WeirdBlog.Utility;

namespace WeirdBlog.Models
{
    public class CategoryEighteen
    {
        public int Id { get; set; }
        [Required]
        [StringLength(StaticConstants.CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
