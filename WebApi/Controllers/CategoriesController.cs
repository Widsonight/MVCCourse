using AutoMapper;
using CoreBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases.CategoriesUseCases;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IViewCategoriesUseCase viewCategoriesUseCase;
        private readonly IViewSelectedCategoryUseCase viewSelectedCategoryUseCase;
        private readonly IEditCategoryUseCase editCategoryUseCase;
        private readonly IAddCategoryUseCase addCategoryUseCase;
        private readonly IDeleteCategoryUseCase deleteCategoryUseCase;
        private readonly IMapper _mapper;

        public CategoriesController(
            IViewCategoriesUseCase viewCategoriesUseCase,
            IViewSelectedCategoryUseCase viewSelectedCategoryUseCase,
            IEditCategoryUseCase editCategoryUseCase,
            IAddCategoryUseCase addCategoryUseCase,
            IDeleteCategoryUseCase deleteCategoryUseCase,
            IMapper mapper)
        {
            this.viewCategoriesUseCase = viewCategoriesUseCase;
            this.viewSelectedCategoryUseCase = viewSelectedCategoryUseCase;
            this.editCategoryUseCase = editCategoryUseCase;
            this.addCategoryUseCase = addCategoryUseCase;
            this.deleteCategoryUseCase = deleteCategoryUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public Task<IEnumerable<CategoryView>> Get()
        {
            var categories = viewCategoriesUseCase.Execute();
            var catgs=_mapper.Map<IEnumerable<CategoryView>>(categories);
            return Task.FromResult(catgs);
        }

        [HttpGet("{id}")]
        public Task<CategoryView> Get(int id)
        {
            var categories = viewSelectedCategoryUseCase.Execute(id);
            var catg = _mapper.Map<CategoryView>(categories);
            return Task.FromResult(catg);
        }

        [HttpPatch, Authorize]
        public Task Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                editCategoryUseCase.Execute(category.CategoryId, category);
            }

            return Task.CompletedTask;
        }

        [Authorize]
        [HttpPost]
        public Task Add(Category category)
        {
            if (ModelState.IsValid)
            {
                addCategoryUseCase.Execute(category);
            }

            return Task.CompletedTask;
        }

        [Authorize]
        [HttpDelete]
        public Task Delete(int categoryId)
        {
            deleteCategoryUseCase.Execute(categoryId);
            return Task.CompletedTask;
        }
    }
}
