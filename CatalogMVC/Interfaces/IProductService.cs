using CatalogMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CatalogMVC.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllByString(string str);
        SelectList GetCategories(object selectedCategory = null);
        bool Add(Product product);
        bool Update(Product product);
        bool Delete(Product product);
        bool Save();
    }
}
