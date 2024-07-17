using CoreBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Factory.Interfaces;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductClient _productClient;
        private readonly ICategoriesClient _categoriesClient;
        public ProductsController(IProductClient productClient,ICategoriesClient categoriesClient)
        {
            _productClient = productClient;
            _categoriesClient = categoriesClient;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productClient.GetProducts();
            return View(products);
        }

        public async Task<IActionResult> Add()
        {
            ViewBag.Action = "add";

            var productViewModel = new ProductViewModel
            {
                Categories = await _categoriesClient.GetCategories()
            };

            return View(productViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                await _productClient.AddProduct(productViewModel.Product);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "add";
            productViewModel.Categories = await _categoriesClient.GetCategories(); ;
            return View(productViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Action = "edit";
            var productViewModel = new ProductViewModel
            {
                Product = await _productClient.GetProduct(id) ?? new Product(),
                Categories = await _categoriesClient.GetCategories()
            };

            return View(productViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                await _productClient.EditProduct(productViewModel.Product);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "edit";
            productViewModel.Categories = await _categoriesClient.GetCategories();
            return View(productViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productClient.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
