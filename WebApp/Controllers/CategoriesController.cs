using Microsoft.AspNetCore.Mvc;
using CoreBusiness;
using Microsoft.AspNetCore.Authorization;
using WebApp.Factory.Interfaces;
using Microsoft.AspNetCore.OutputCaching;

namespace WebApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesClient _categoriesClient;
        public CategoriesController(ICategoriesClient categoriesClient)
        {
            _categoriesClient = categoriesClient;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoriesClient.GetCategories();
            return View(categories);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Action = "edit";

            var category = await _categoriesClient.GetCategorie(id.HasValue ? id.Value : 0);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoriesClient.EditCategorie(category);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "edit";
            return View(category);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoriesClient.AddCategorie(category);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "add";
            return View(category);
        }

        public async Task<IActionResult> Delete(int categoryId)
        {
            await _categoriesClient.DeleteCategorie(categoryId);
            return RedirectToAction(nameof(Index));
        }

    }
}
