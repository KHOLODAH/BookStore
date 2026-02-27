using BookShoppingCartMvcUI.Data;
using BookStore2.Constant;
using Microsoft.AspNetCore.Identity;

namespace BookStore2.Data
{
    public class DbSeeder

    {
       
        public static async Task SeedDefaultData(IServiceProvider service)
        {



            var userMgr = service.GetService<UserManager<IdentityUser>>();
            var roleMgr = service.GetService<RoleManager<IdentityRole>>();
            var db = service.GetRequiredService<ApplicationDbContext>();

            // create admin role if not exists
            var adminRoleExists = await roleMgr.RoleExistsAsync(Roles.Admin.ToString());

            if (!adminRoleExists)
            {
                await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            }

            // create user role if not exists
            var userRoleExists = await roleMgr.RoleExistsAsync(Roles.User.ToString());

            if (!userRoleExists)
            {
                await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));
            }

            if (!db.orderStatuses.Any(s => s.StatusName == "Pending"))
            {
                db.orderStatuses.Add(new OrderStatus { StatusName = "Pending" });
                await db.SaveChangesAsync();
            }
            // create admin user

            var admin = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            var userInDb = await userMgr.FindByEmailAsync(admin.Email);
            if (userInDb is null)
            {
                await userMgr.CreateAsync(admin, "Admin@123");
                await userMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
        }
    }
}