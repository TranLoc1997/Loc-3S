using System.Threading.Tasks;
using TaskUser.Encryption;
using TaskUser.Models;
using TaskUser.Models.Sales;
using TaskUser.Service;
using TaskUser.ViewsModels.User;
using Xunit;

namespace Tests.ServiceTest
{
    public class TestUserService
    {
        private readonly DataContext _dataContext;
        private readonly UserService _userServices;

        public TestUserService()
        {
            _dataContext = TestHelpers.GetDataContext();
            AutoMapperConfig.Initialize();
            var mapper = AutoMapperConfig.GetMapper();
            _userServices = new UserService(_dataContext, mapper);
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
        [Fact]
        public async Task GetUser_ReturnListUser()
        {
            DbSeed();
            var users = await _userServices.GetUserListAsync();
            Assert.Equal(users.Count, 1);
        }

        /// <summary>
        /// GetUser_ReturnNull
        /// </summary>
        /// <returns>Null</returns>
        [Fact]
        public async Task GetUser_ReturnUserNull()
        {
            var users = await _userServices.GetUserListAsync();
            Assert.Equal(users.Count, 0);
        }
        [Fact]
        public void Login_ReturnTrue()
        {
            DbSeed();
            const string email = "Vanloc@gmail.com";
            const string password = "10101997a";
            var isLogin =  _userServices.Login(email, password);
            Assert.True(isLogin);
        }

        /// <summary>
        /// Login_ReturnFalse
        /// </summary>
        [Fact]
        public void Login_ReturnFalse()
        {
            DbSeed();
            const string email = "Vanloc10101997@gmail.com";
            const string password = "Aa1234566";
            var isLogin = _userServices.Login(email, password);
            Assert.False(isLogin);
        }
        
        /// <summary>
        /// Add_ReturnTrue
        /// </summary>
        /// <returns>true</returns>
        [Fact]
        public async Task Add_ReturnTrue()
        {
            DbSeed();
            var user = new UserViewsModels
            {
                StoreId = 1,
                Email = "Vanloc@gmail.com",
                PassWord = "10101997a",
                Name = "Loc",
                Phone = "0768407899",
                Role = UserViewsModels.RoleName.Admin,
                IsActiver = true,
            };
            var result = await _userServices.AddUserAsync(user);
            Assert.True(result);
        }
        /// <summary>
        /// Add_ReturnTrue
        /// </summary>
        /// <returns>False</returns>
        [Fact]
        public async Task Add_ReturnFalse()
        {
            DbSeed();
            var user = new UserViewsModels();
            var result = await _userServices.AddUserAsync(user);
            Assert.False(result);
        }
        /// <summary>
        /// GetId_ReturnUser
        /// </summary>
        /// <returns>User</returns>
        
        [Fact]
        public async Task GetId_ReturnUser()
        {
            DbSeed();
            const int id = 1;
            var user = await _userServices.GetIdAsync(id);
            Assert.NotNull(user);
        }

        /// <summary>
        /// GetId_ReturNull
        /// </summary>
        /// <returns>Null</returns>
        [Fact]
        public async Task GetId_ReturnNull()
        {
            DbSeed();
            const int id = 0;
            var user = await _userServices.GetIdAsync(id);
            Assert.Null(user);
        }
        /// <summary>
        /// Edit User
        /// </summary>
        /// <returns>True</returns>
        [Fact]
        public async Task Edit_ReturnTrue()
        {
            DbSeed();
            var user = new EditUserViewsModels()
            {
                Id = 1,
                Email = "Vanloc@gmail.com",
                Name = "Loc",
                Phone = "0768407899",
                IsActiver = true,
                Role = EditUserViewsModels.RoleName.Admin
            };    
            var result = await _userServices.EditUserAsync(user); 
            Assert.True(result);
        }
        
        /// <summary>
        /// Edit User
        /// </summary>
        /// <returns>False</returns>
        [Fact]
        public async Task Edit_ReturnFalse()
        {
            DbSeed();
            var user = new EditUserViewsModels
            {
                Id = 0,
                Email = "tuan1@gmail.com",
                Name = "Dinh Viet Tuan",
                Phone = "0768407899",
                IsActiver = true,
                Role = EditUserViewsModels.RoleName.Admin
            };    
            var result = await _userServices.EditUserAsync(user); 
            Assert.False(result);
        }
        /// <summary>
        /// EditPassword
        /// </summary>
        /// <returns>True</returns>
        [Fact]
        public async Task EditPassword_ReturnTrue()
        {
            DbSeed();
            var user = new EditViewPassword
            {
                Id = 1,
                NewPassword = "10101997a"
            };
            var result = await _userServices.EditPasswordAsync(user);
            Assert.True(result);
        }
        
        /// <summary>
        /// EditPassword
        /// </summary>
        /// <returns>False</returns>
        [Fact]
        public async Task EditPassword_ReturnFalse()
        {
            DbSeed();
            var user = new EditViewPassword
            {
                Id = 0,
                NewPassword = "loc123456"
            };
            var result = await _userServices.EditPasswordAsync(user);
            Assert.False(result);
        }
        /// <summary>
        /// GetId_ReturnPasswordUser
        /// </summary>
        /// <returns>not null</returns>
        
        [Fact]
        public async Task GetId_ReturnPasswordUser()
        {
            DbSeed();
            const int id = 1;
            var user = await _userServices.GetPasswordAsync(id);
            Assert.NotNull(user);
        }

        /// <summary>
        /// GetId_ReturNull
        /// </summary>
        /// <returns>Null</returns>
        [Fact]
        public async Task GetId_ReturnPasswordNull()
        {
            DbSeed();
            const int id = 0;
            var user = await _userServices.GetPasswordAsync(id);
            Assert.Null(user);
        }
        /// <summary>
        /// Delete User
        /// </summary>
        /// <returns>True</returns>
        [Fact]
        public async Task Delete_ReturnTrue()
        {
            DbSeed();
            const int id = 1;
            var user = await _userServices.Delete(id);
            Assert.True(user);
        }
        
        /// <summary>
        /// Delete User
        /// </summary>
        /// <returns>False</returns>
        [Fact]
        public async Task Delete_ReturnFalse()
        {
            DbSeed();
            const int id = 0;
            var user = await _userServices.Delete(id);
            Assert.False(user);
        }

    }
}