using AutoMapper;
using TaskUser.ViewsModels.Category;
using TaskUser.ViewsModels.Product;
using TaskUser.ViewsModels.Stock;
using TaskUser.ViewsModels.Store;
using TaskUser.ViewsModels.User;

namespace TaskUser.ViewsModels
{
    public class MapProfile :Profile
    {
        public MapProfile()
        {
            CreateMap<Models.Production.Brand, Brand.BrandViewsModels>();
            CreateMap<Models.Production.Category, CategoryViewsModels>();
            CreateMap<Models.Production.Product, ProductViewsModels>();
            CreateMap<Models.Production.Stock, StockViewModels>();
            CreateMap<Models.Sales.Store, StoreViewModels>();
            CreateMap<Models.Sales.User, UserViewsModels>();
            CreateMap<Models.Sales.User, LoginViewModel>();
            CreateMap<Models.Sales.User, EditViewPassword>();
            CreateMap<Models.Sales.User, EditUserViewsModels>();
        }
    }
}