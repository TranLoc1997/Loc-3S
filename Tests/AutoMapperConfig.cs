using AutoMapper;
using TaskUser.Models.Production;
using TaskUser.Models.Sales;
using TaskUser.ViewsModels.Brand;
using TaskUser.ViewsModels.Category;
using TaskUser.ViewsModels.Product;
using TaskUser.ViewsModels.Stock;
using TaskUser.ViewsModels.Store;
using TaskUser.ViewsModels.User;

namespace Tests
{
    public static class AutoMapperConfig
    {
        private static readonly object ThisLock = new object();
        private static bool _initialized;

        private static IMapper _mapper;

        // Centralize automapper initialize
        public static void Initialize()
        {
            // This will ensure one thread can access to this static initialize call
            // and ensure the mapper is reseted before initialized
            lock (ThisLock)
            {
                if (!_initialized)
                {
                    var config = new MapperConfiguration(opts =>
                    {
                        opts.CreateMap<Brand, BrandViewsModels>();
                        opts.CreateMap<Category, CategoryViewsModels>();
                        opts.CreateMap<Product, ProductViewsModels>();
                        opts.CreateMap<Store, StoreViewModels>();
                        opts.CreateMap<Stock, StockViewModels>();

                        opts.CreateMap<User, LoginViewModel>();
                        opts.CreateMap<User, EditViewPassword>();
                        opts.CreateMap<User, EditUserViewsModels>();
                        opts.CreateMap<User, UserViewsModels>();
                    });
                    _initialized = true;
                    _mapper = config.CreateMapper();
                }
            }
        }

        public static IMapper GetMapper()
        {
            return _mapper;
        }
    }
}