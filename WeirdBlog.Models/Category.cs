﻿using System.ComponentModel.DataAnnotations;
using WeirdBlog.Utility;

namespace WeirdBlog.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(StaticConstants.CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
