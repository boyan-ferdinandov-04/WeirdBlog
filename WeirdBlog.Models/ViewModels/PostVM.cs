using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WeirdBlog.Models.ViewModels
{
    public class PostVM
    {
        public Post Post { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; }
        [ValidateNever]
        public string SearchTitle { get; set; }
        [ValidateNever]
        public int? SelectedCategoryId { get; set; }
    }
}
