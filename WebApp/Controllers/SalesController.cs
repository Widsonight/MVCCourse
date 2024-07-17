using Microsoft.AspNetCore.Mvc;
using CoreBusiness;
using WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using WebApp.Factory.Interfaces;

namespace WebApp.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISalesClient _salesClient;
        private readonly IProductClient _productClient;
        private readonly ICategoriesClient _categoriesClient;

        public SalesController(ISalesClient salesClient, IProductClient productClient, ICategoriesClient categoriesClient)
        {
            _salesClient = salesClient;
            _productClient = productClient;
            _categoriesClient = categoriesClient;
        }

        public async Task<IActionResult> Index()
        {
            var salesViewModel = new ViewModels.SalesViewModel
            {
                Categories = await _categoriesClient.GetCategories()
            };
            return View(salesViewModel);
        }

        public async Task<IActionResult> SellProductPartial(int productId)
        {
            var product = await _productClient.GetProduct(productId);
            return PartialView("_SellProduct", product);
        }

        public async Task<IActionResult> Sell(ViewModels.SalesViewModel salesViewModel)
        {
            if (ModelState.IsValid)
            {
                // Sell the product
                await _salesClient.AddSalesViewModel(
                    salesViewModel);
            }

            var product = await _productClient.GetProduct(salesViewModel.SelectedProductId);
            salesViewModel.SelectedCategoryId = (product?.CategoryId == null) ? 0 : product.CategoryId.Value;
            salesViewModel.Categories = await _categoriesClient.GetCategories();

            return View("Index", salesViewModel);
        }

        public async Task<IActionResult> ProductsByCategoryPartial(int categoryId)
        {
            var products = await _salesClient.GetProductById(categoryId);

            return PartialView("_Products", products);
        }
    }
}
