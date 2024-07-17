using AutoMapper;
using CoreBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases;
using UseCases.CategoriesUseCases;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IAddProductUseCase addProductUseCase;
        private readonly IEditProductUseCase editProductUseCase;
        private readonly IDeleteProductUseCase deleteProductUseCase;
        private readonly IViewSelectedProductUseCase viewSelectedProductUseCase;
        private readonly IViewProductsUseCase viewProductsUseCase;
        private readonly IViewCategoriesUseCase viewCategoriesUseCase;
        private readonly IMapper _mapper;

        public ProductsController(
            IAddProductUseCase addProductUseCase,
            IEditProductUseCase editProductUseCase,
            IDeleteProductUseCase deleteProductUseCase,
            IViewSelectedProductUseCase viewSelectedProductUseCase,
            IViewProductsUseCase viewProductsUseCase,
            IViewCategoriesUseCase viewCategoriesUseCase,IMapper mapper)
        {
            this.addProductUseCase = addProductUseCase;
            this.editProductUseCase = editProductUseCase;
            this.deleteProductUseCase = deleteProductUseCase;
            this.viewSelectedProductUseCase = viewSelectedProductUseCase;
            this.viewProductsUseCase = viewProductsUseCase;
            this.viewCategoriesUseCase = viewCategoriesUseCase;
            _mapper= mapper;
            
        }

        [HttpGet]
        public Task<IEnumerable<ProductView>> Get()
        {
            var products = viewProductsUseCase.Execute(loadCategory: true);
            var prods = _mapper.Map<IEnumerable<ProductView>>(products);
            
            return Task.FromResult(prods);
        }


        [HttpGet("{id}")]
        public Task<ProductView> Get(int id)
        {
            var product = viewSelectedProductUseCase.Execute(id);
            var prod = _mapper.Map<ProductView>(product);
            return Task.FromResult(prod);
        }

        [HttpPost]
        public Task Add(Product product)
        {
            if (ModelState.IsValid)
            {
                addProductUseCase.Execute(product);
            }
            return Task.CompletedTask;
        }

        [HttpPatch]
        public Task Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                editProductUseCase.Execute(product.ProductId, product);
            }
            return Task.CompletedTask;
        }

        [HttpDelete]
        public Task Delete([FromQuery] int id)
        {
            deleteProductUseCase.Execute(id);
            return Task.CompletedTask;
        }
    }
}
