using CoreBusiness;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class CategoryModel
    {
        public int categoryId { get; set; }

        [Required]
        public string name { get; set; } = string.Empty;
        public string? description { get; set; } = string.Empty;

        // navigation property for ef core
        public List<Product>? products { get; set; }
    }
}
