using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeirdBlog.Models.ViewModels
{
    public class PostEighteenVM
    {
        public PostEighteen Post { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; }
        [ValidateNever]
        public string SearchTitle { get; set; }
        [ValidateNever]
        public int? SelectedCategoryId { get; set; }
    }
}
