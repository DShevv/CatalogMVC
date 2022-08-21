using CatalogMVC.Data;
using CatalogMVC.Interfaces;
using CatalogMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace CatalogMVC.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult>  Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            IEnumerable<Category> categories = await _categoryService.GetAll();
            return View(categories);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Category category = await _categoryService.GetByIdAsync(id);
            return View(category);
        }
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("admin"))
            {
                return View();
            }

            return RedirectToAction("Index");
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
           
                if (ModelState.IsValid)
                {
                    _categoryService.Add(category);
                    return RedirectToAction("Index");
                }
          
            
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (User.Identity.IsAuthenticated && User.IsInRole("admin"))
            {

                if (category == null)
                {
                    return View("Error");
                }
                return View(category);

            }
      
            return RedirectToAction("Index");

            

        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
            var editCategory = await _categoryService.GetByIdAsync(id);
            if (ModelState.IsValid)
            {
                if (editCategory != null)
                {
                    if(await TryUpdateModelAsync<Category>(editCategory,"", c => c.Name, c => c.Products ))
                    {
                        if (!_categoryService.Save())
                        {
                            ModelState.AddModelError("", "Не удалось сохранить изменения. " +
                                "Попробуйте ещё раз или " +
                                "обратитесь к своему администратору");
                        }
                        return RedirectToAction("Index");
                    }
                    
                }
                else
                {        
                    return View(editCategory);
                }
            }
            ModelState.AddModelError("", "Не удалось изменить категорию");
            return View("Edit", editCategory);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var categoryInfo = await _categoryService.GetByIdAsync(id);
            if (User.Identity.IsAuthenticated && User.IsInRole("admin"))
            {
                if (categoryInfo == null)
                {
                    return View("Error");
                }
                return View(categoryInfo);

            }
            return RedirectToAction("Index");

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryInfo = await _categoryService.GetByIdAsync(id);

            if (categoryInfo == null)
            {
                return View("Error");
            }

            _categoryService.Delete(categoryInfo);
            return RedirectToAction("Index");
        }

    }
}
