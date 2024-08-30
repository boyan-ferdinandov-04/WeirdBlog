using Microsoft.AspNetCore.Mvc;
using WeirdBlog.Models;

namespace WeirdBlog.Service
{
    public interface ICategoryService
    {
        List<Category> GetCategories(); 
        List<CategoryEighteen> GetAllCategoriesEighteen();
        void AddCategory(Category category);
        Task<Category> GetCategoryById(int id);
        Task<Category> Edit(Category category);
        Task<bool> Delete(int id);
    }
}
