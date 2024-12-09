using WeirdBlog.DataAccess.Data;
using WeirdBlog.Models;
using WeirdBlog.Service.IService;

namespace WeirdBlog.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly WeirdBlogDbContext _context;
        public CategoryService(WeirdBlogDbContext dbContext)
        {
            _context = dbContext;
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Category> Edit(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }
    }
}
