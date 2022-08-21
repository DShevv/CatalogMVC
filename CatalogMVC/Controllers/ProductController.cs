using CatalogMVC.Interfaces;
using CatalogMVC.Models;
using CatalogMVC.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace CatalogMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login","Account");
            }

            IEnumerable<Product> products = await _productService.GetAll();

            ViewData["CurrentFilter"] = searchString;
            ViewData["NameSortParam"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["CatSortParam"] = sortOrder == "Category" ? "cat_desc" : "Category";
            ViewData["DescSortParam"] = sortOrder == "Description" ? "desc_desc" : "Description";
            ViewData["PriceSortParam"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["PublicSortParam"] = sortOrder == "Public" ? "public_desc" : "Public";
            ViewData["PrivateSortParam"] = sortOrder == "Private" ? "private_desc" : "Private";

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                products = products.Where(p => p.Name.ToUpper().Contains(searchString) ||
                                          p.Category.Name.ToUpper().Contains(searchString) ||
                                          p.Description.ToUpper().Contains(searchString) ||
                                          p.Price.ToString().ToUpper().Contains(searchString) ||
                                          p.PublicNote.ToUpper().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "cat_desc":
                    products = products.OrderByDescending(p => p.Category.Name);
                    break;
                case "desc_desc":
                    products = products.OrderByDescending(p => p.Description);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "public_desc":
                    products = products.OrderByDescending(p => p.PublicNote);
                    break;
                case "private_desc":
                    products = products.OrderByDescending(p => p.PrivateNote);
                    break;
                case "Name":
                    products = products.OrderBy(p => p.Name);
                    break;
                case "Category":
                    products = products.OrderBy(p => p.Category.Name);
                    break;
                case "Description":
                    products = products.OrderBy(p => p.Description);
                    break;
                case "Price":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "Public":
                    products = products.OrderBy(p => p.PublicNote);
                    break;
                case "Private":
                    products = products.OrderBy(p => p.PrivateNote);
                    break;
                default:
                    products = products;
                    break;
                
            }


            return View(products);

        }

        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Advanced"))
            {
                PopulateDepartmentsDropDownList();
                return View();
            }
            return RedirectToAction("Index");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                PopulateDepartmentsDropDownList(product.CategoryId);
                return View(product);
            }
            _productService.Add(product);
            return RedirectToAction("Index");
        }


        public  void PopulateDepartmentsDropDownList(object selectedCategory = null)
        {
            SelectList selectList = _productService.GetCategories(selectedCategory);
            ViewBag.CategoryId = selectList;
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Advanced"))
            {
                var product = await _productService.GetByIdAsync(id);

                if (product == null)
                {
                    return View("Error");
                }
                PopulateDepartmentsDropDownList(product.CategoryId);
                return View(product);
            }
            return RedirectToAction("Index");
            
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
            var editProduct = await _productService.GetByIdAsync(id);
            if (ModelState.IsValid)
            {


                if (editProduct != null)
                {

                    if (await TryUpdateModelAsync<Product>(editProduct, "", c => c.Name, c => c.Category,c=> c.CategoryId, c => c.Description, c => c.Price, c => c.PublicNote, c => c.PrivateNote))
                    {
                        if (!_productService.Save())
                        {
                            ModelState.AddModelError("", "Unable to save changes. " +
                                "Try again, and if the problem persists, " +
                                "see your system administrator.");
                        }

                        return RedirectToAction("Index");


                    }

                }
                else
                {
                    PopulateDepartmentsDropDownList(editProduct.CategoryId);
                    return View(editProduct);
                }


            }
            ModelState.AddModelError("", "Не удалось изменить категорию");
            return View("Edit", editProduct);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Advanced"))
            {
                var product = await _productService.GetByIdAsync(id);

                if (product == null)
                {
                    return View("Error");
                }
                PopulateDepartmentsDropDownList(product.CategoryId);
                return View(product);
            }
            return RedirectToAction("Index");
            
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return View("Error");
            }

            _productService.Delete(product);
            return RedirectToAction("Index");
        }

    }
}
