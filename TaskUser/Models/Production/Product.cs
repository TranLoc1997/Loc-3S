using System.Collections.Generic;

namespace TaskUser.Models.Production
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Picture { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int  ModelYear { get; set; }
        public decimal ListPrice { get; set; }
        
       public virtual Category Categorie { get; set; }
        public virtual ICollection<Stock>Stocks { get; set; }
        public virtual Brand Brand { get; set; }
        
    }
}


