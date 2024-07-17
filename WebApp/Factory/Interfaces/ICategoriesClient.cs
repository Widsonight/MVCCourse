using CoreBusiness;

namespace WebApp.Factory.Interfaces
{
    public interface ICategoriesClient
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategorie(int id);
        Task AddCategorie(Category category);
        Task EditCategorie(Category category);
        Task DeleteCategorie(int id);
    }
}
