using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeirdBlog.Utility;

namespace WeirdBlog.Models
{
    public class Post
    {
        [Key]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(StaticConstants.PostTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        
    }
}
