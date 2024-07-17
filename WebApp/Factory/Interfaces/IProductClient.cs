using CoreBusiness;
using WebApp.ViewModels;

namespace WebApp.Factory.Interfaces
{
    public interface IProductClient
    {
        Task AddProduct(Product prod);
        Task DeleteProduct(int id);
        Task EditProduct(Product category);
        Task<Product> GetProduct(int id);
        Task<IEnumerable<Product>> GetProducts();
    }
}