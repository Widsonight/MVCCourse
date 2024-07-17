using CoreBusiness;
using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModel
{
    public class ProductView
    {
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int? Quantity { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public double? Price { get; set; }

        // navigation property for ef core
        public CategoryView? Category { get; set; }
    }
}
