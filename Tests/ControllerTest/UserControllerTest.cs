using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using TaskUser.Controllers;
using TaskUser.Encryption;
using TaskUser.Models;
using TaskUser.Models.Sales;
using TaskUser.Resources;
using TaskUser.Service;
using TaskUser.ViewsModels.User;
using Xunit;

namespace Tests.ControllerTest
{
    
    public class UserControllerTest
    {
        private readonly UserController _userController;
        private readonly DataContext _dataContext;

        public UserControllerTest()
        {
            
            _dataContext = TestHelpers.GetDataContext();
            AutoMapperConfig.Initialize();
            var mapper = AutoMapperConfig.GetMapper();
            
            var options = Options.Create(new LocalizationOptions());  // you should not need any params here if using a StringLocalizer<T>
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            
            var userService = new UserService(_dataContext, mapper);
            var storeService = new StoreService(_dataContext, mapper);
            var localizer = new SharedViewLocalizer<CommonResource>(factory);
            
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["Successfuly"] = localizer.GetLocalizedString("msg_AddSuccessfuly")
            };

            _userController = new UserController(userService,storeService,localizer)
            {
                TempData = tempData
            };
        }
        private void DbSeed()
        {
            var user = new User
            {
                Email = "Vanloc@gmail.com",
                PassWord = SecurePasswordHasher.Hash("10101997a"),
                Name = "Loc",
                StoreId=1,
                Phone = "123456789",
                Role = 1,
                IsActiver = true
            };
            _dataContext.Users.Add(user);

            var store = new Store
            {
                StoreName = "Tui sach",
                Email = "Vanloc@gmail.com",
                City = "hue",
                Phone = "123456789",
                Street = "Ha Noi",
                State = "khong",
                ZipCode = "51634"
            };
            _dataContext.Stores.Add(store);
            _dataContext.SaveChanges();

        }
        /// <summary>
        /// Index Test
        /// </summary>
        [Fact]
        public async Task Index_ReturnViewIndex()
        {
            var listUserResult = await _userController.Index();
            Assert.IsType<ViewResult>(listUserResult);
        }
        /// <summary>
        /// Get Create Test
        /// </summary>
        [Fact]
        public void  GetCreate_ReturnView()
        {
            var result = _userController.Create();
            Assert.IsType<ViewResult>(result);
        }
        
        /// <summary>
        /// Post Create Test
        /// </summary>
        /// <returns>RedirectToActionResult</returns>
        [Fact]
        public async Task PostCreate_ReturnRedirectToAction()
        {
            var user = new UserViewsModels
            {
                Email = "Vanloc@gmail.com.vn",
                PassWord = "10101997",
                Name = "Loc",
                StoreId = 1,
                Phone = "10101997",
                Role =UserViewsModels.RoleName.Admin,
                IsActiver = true,
            };
            var result = await _userController.Create(user);
            Assert.IsType<RedirectToActionResult>(result);
        }
        [Fact]
        public void GetEdit_ReturnBadRequest()
        {
            var result = _userController.Edit(0);
            Assert.IsType<BadRequestResult>(result.Result);
        }
        
        /// <summary>
        /// Get Edit Test
        /// </summary>
        [Fact]
        public async Task GetEdit_ReturnViewResult()
        {
            var user = new EditUserViewsModels
            {
                Id = 1,
                Email = "Vanloc@gmail.com",
                Name = "Loc",
                Phone = "0768407899",
                IsActiver = true,
                Role = EditUserViewsModels.RoleName.Admin
            };
            //const int id = 1;
            var result = await _userController.Edit(user);
            Assert.IsType<ViewResult>(result);
        }
        
        /// <summary>
        /// Post Edit Test
        /// </summary>
        /// <returns>ViewResult</returns>
        [Fact]
        public async Task PostEdit_ReturnViewResult()
        {
            var user = new EditUserViewsModels();
            var result = await _userController.Edit(user);
            Assert.IsType<ViewResult>(result);
        }
       
        /// <summary>
        /// Post Edit Test
        /// </summary>
        /// <returns>RedirectToActionResult</returns>
        [Fact]
        public async Task PostEdit_ReturnRedirectToAction()
        {
            var user = new EditUserViewsModels()
            {
                Id = 1,
                Email = "Loc@3si.com.vn",
                Name = "Loc",
                StoreId = 1,
                Phone = "191960875",
                Role = EditUserViewsModels.RoleName.Admin,
                IsActiver = true,
            };
            var result = await _userController.Edit(user);
            Assert.IsType<ViewResult>(result);
        }
        /// <summary>
        /// postedit error
        /// </summary>
        /// <returns>view</returns>
        [Fact]
        public async Task PostEdit_ReturnRedirectToActionError()
        {
            var user = new EditUserViewsModels();
            var result = await _userController.Edit(user);
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async Task GetChangePassword_ReturnBadRequest()
        {           
            DbSeed();
            const int id = 0;
            var result = await _userController.EditPassword(id);
            Assert.IsType<BadRequestResult>(result);
        }
        
        /// <summary>
        /// Get ChangePassword Test
        /// </summary>
        [Fact]
        public void GetChangePassword_ReturnViewResult()
        {
            DbSeed();
            const int id = 1;
            var result = _userController.EditPassword(id);
            Assert.IsType<PartialViewResult>(result);
        }
        
        /// <summary>
        /// Post ChangePassword Test
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostChangePassword_ReturnViewResultError()
        {
            var user = new EditViewPassword();
            var result = await _userController.EditPassword(user);
            Assert.IsType<PartialViewResult>(result);
        }
       
        /// <summary>
        /// Post ChangePassword Test
        /// </summary>
        /// <returns>RedirectToActionResult</returns>
        [Fact]
        public async Task PostChangePassword_ReturnRedirectToAction()
        {
            var user = new EditViewPassword()
            {
                Id = 1,
                NewPassword = "10101997"
            };
            var result = await _userController.EditPassword(user);
            Assert.IsType<PartialViewResult>(result);
        }
        /// <summary>
        /// Get Delete Test
        /// </summary>
        [Fact]
        public void GetDelete_ReturnBadRequestError()
        {
            var result = _userController.Delete(null);
            Assert.IsType<BadRequestResult>(result.Result);
        }
        
        /// <summary>
        /// Get Delete Test
        /// </summary>
        [Fact]
        public void GetDelete_ReturnViewResult()
        {
            var result = _userController.Delete(1);
            Assert.IsType<RedirectToActionResult>(result.Result);
        }



    }
}