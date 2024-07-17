using CoreBusiness;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases;
using UseCases.CategoriesUseCases;
using UseCases.ProductsUseCases;
using CoreBusiness;
using WebApi.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly IViewCategoriesUseCase viewCategoriesUseCase;
        private readonly IViewSelectedProductUseCase viewSelectedProductUseCase;
        private readonly ISellProductUseCase sellProductUseCase;
        private readonly IViewProductsInCategoryUseCase viewProductsInCategoryUseCase;
        private IMapper _mapper;

        public SalesController(IViewCategoriesUseCase viewCategoriesUseCase,
            IViewSelectedProductUseCase viewSelectedProductUseCase,
            ISellProductUseCase sellProductUseCase,
            IViewProductsInCategoryUseCase viewProductsInCategoryUseCase,IMapper mapper)
        {
            this.viewCategoriesUseCase = viewCategoriesUseCase;
            this.viewSelectedProductUseCase = viewSelectedProductUseCase;
            this.sellProductUseCase = sellProductUseCase;
            this.viewProductsInCategoryUseCase = viewProductsInCategoryUseCase;
            _mapper = mapper;
        }
        //[HttpGet]
        //public Task<IEnumerable<ProductView>> Get()
        //{
        //    var categories = viewCategoriesUseCase.Execute();
        //    var catgs = _mapper.Map<IEnumerable<ProductView>>(categories);
        //    return Task.FromResult(catgs);
        //}

        //[HttpGet("products")]
        //public Task SellProductPartial(int productId)
        //{
        //    var product = viewSelectedProductUseCase.Execute(productId);
        //    return Task.FromResult(product);
        //}

        [HttpPost]
        public Task Sell(SalesViewModel salesModel)
        {
            if (ModelState.IsValid)
            {
                // Sell the product
                sellProductUseCase.Execute(
                    "cashier1",
                    salesModel.SelectedProductId,
                    salesModel.QuantityToSell);
            }

            var product = viewSelectedProductUseCase.Execute(salesModel.SelectedProductId);
            salesModel.SelectedCategoryId = (product?.CategoryId == null) ? 0 : product.CategoryId.Value;
            salesModel.Categories = viewCategoriesUseCase.Execute();

            return Task.FromResult(salesModel);
        }

        [HttpGet("{categoryId}")]
        public Task<IEnumerable<ProductView>> ProductsByCategoryPartial(int categoryId)
        {
            var products = viewProductsInCategoryUseCase.Execute(categoryId);
            var prds = _mapper.Map<IEnumerable<ProductView>>(products);

            return Task.FromResult(prds);
        }
    }
}
