using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace TaskUser.Controllers
{
    public class RoleController
    {
        private async Task CreateRoles(IServiceProvider serviceProvider)  
        {  
            //initializing custom roles   
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();  
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();  
            string[] roleNames = { "Admin", "User", "HR" };  
            IdentityResult roleResult;  
  
            foreach (var roleName in roleNames)  
            {  
                var roleExist = await RoleManager.RoleExistsAsync(roleName);  
                if (!roleExist)  
                {  
                    //create the roles and seed them to the database: Question 1  
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));  
                }  
            }  
  
            IdentityUser user = await UserManager.FindByEmailAsync("jignesh@gmail.com");  
  
            if (user == null)  
            {  
                user = new IdentityUser()  
                {  
                    UserName = "jignesh@gmail.com",  
                    Email = "jignesh@gmail.com",  
                };  
                await UserManager.CreateAsync(user, "Test@123");  
            }  
            await UserManager.AddToRoleAsync(user, "Admin");  
  
  
            IdentityUser user1 = await UserManager.FindByEmailAsync("tejas@gmail.com");  
  
            if (user1 == null)  
            {  
                user1 = new IdentityUser()  
                {  
                    UserName = "tejas@gmail.com",  
                    Email = "tejas@gmail.com",  
                };  
                await UserManager.CreateAsync(user1, "Test@123");  
            }  
            await UserManager.AddToRoleAsync(user1, "User");  
  
            IdentityUser user2 = await UserManager.FindByEmailAsync("rakesh@gmail.com");  
  
            if (user2 == null)  
            {  
                user2 = new IdentityUser()  
                {  
                    UserName = "rakesh@gmail.com",  
                    Email = "rakesh@gmail.com",  
                };  
                await UserManager.CreateAsync(user2, "Test@123");  
            }  
            await UserManager.AddToRoleAsync(user2, "HR");  
  
        }  
    }
}