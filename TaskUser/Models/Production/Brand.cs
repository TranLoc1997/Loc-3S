using System.Collections.Generic;

namespace TaskUser.Models.Production
{
    public class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        
        public virtual ICollection<Product>Product { get; set; }
    }
}
