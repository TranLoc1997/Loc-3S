using System.Reflection;
using Microsoft.Extensions.Localization;

namespace TaskUser.Resources
{
    public class SharedViewLocalizer<T>
    {
        
        private readonly IStringLocalizer _localizer;

        public SharedViewLocalizer(IStringLocalizerFactory factory)
        {
            var type = typeof(T);
            var assemblyName = type.GetTypeInfo().Assembly.GetName().Name;
            var typeName = type.Name;
            _localizer = factory.Create(typeName, assemblyName);
        }

        public LocalizedString GetLocalizedString(string key)
        {
            return _localizer[key];
        }

        

    }
//    public class BrandResource{}
//    public class CategoryResource{}
//    public class ProductResource{}
//    public class LoginResource{}
//    public class StockResource{}
//    public class CommonResource{}
//    public class StoreResource{}
//    public class UserResource{}
//    public class PasswordResource{}
 
}