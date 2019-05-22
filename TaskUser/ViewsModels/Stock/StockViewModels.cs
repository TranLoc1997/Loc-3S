namespace TaskUser.ViewsModels.Stock
{
    public class StockViewModels
    {
        public int Quantity { get; set; }
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        
        public virtual Models.Sales.Store Store { get; set; }
        public virtual Models.Production.Product Product { get; set; }
    }
}