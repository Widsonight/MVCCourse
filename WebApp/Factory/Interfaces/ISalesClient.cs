using WebApp.ViewModels;
using CoreBusiness;
namespace WebApp.Factory.Interfaces
{
    public interface ISalesClient
    {
        Task AddSalesViewModel(ViewModels.SalesViewModel prod);
        Task<IEnumerable<Product>> GetProductById(int categoryId);
    }
}