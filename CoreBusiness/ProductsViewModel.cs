using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusiness
{
    public class ProductsViewModel
    {
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public  Product Product { get; set; } = new Product();
    }
}
