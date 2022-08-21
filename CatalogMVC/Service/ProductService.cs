using CatalogMVC.Data;
using CatalogMVC.Interfaces;
using CatalogMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CatalogMVC.Service
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Product product)
        {
            _context.Add(product);
            return Save();
        }

        public bool Delete(Product product)
        {
            _context.Remove(product);
            return Save();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllByString(string str)
        {
            return await _context.Products.Where(p =>
                p.Name.Contains(str) ||
                p.Category.Name.Contains(str) ||
                p.PublicNote.Contains(str) ||
                p.Description.Contains(str)
            ).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
        }

        public SelectList GetCategories(object selectedCategory = null)
        {
            var categoriesQuery = from c in _context.Categories
                                  orderby c.Name
                                  select c;
            
            return new SelectList(categoriesQuery.AsNoTracking(), "Id", "Name", selectedCategory);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0;
        }

        public bool Update(Product product)
        {
            _context.Update(product);
            return Save();
        }
    }
}
