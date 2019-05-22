using TaskUser.Models.Sales;

namespace TaskUser.Models.Production
{
    public class Stock
    {
//        public int Id { get; set; }
       
        public int Quantity { get; set; }
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        
        public virtual Store Store { get; set; }
       public virtual Product Product { get; set; }
    }
}