using CatalogMVC.Data;
using CatalogMVC.Interfaces;
using CatalogMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogMVC.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationDbContext Context { get; }

        public bool Add(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool Delete(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.Include(i => i.Products).FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0;
        }

        public bool Update(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
