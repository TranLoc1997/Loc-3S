using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TaskUser.ViewsModels.Product
{
    public class ProductViewsModels
    {
        

//       
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int  ModelYear { get; set; }
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public decimal ListPrice { get; set; }
        public string Picture { get; set; }
        public IFormFile  PictureFile { get; set; }
        
        public virtual Models.Production.Category Categorie { get; set; }
        public virtual ICollection<Models.Production.Stock>Stocks { get; set; }
        public virtual Models.Production.Brand Brand { get; set; }
    }
}