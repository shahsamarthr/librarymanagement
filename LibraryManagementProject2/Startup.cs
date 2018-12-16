using LibraryManagementProject2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibraryManagementProject2.Startup))]
namespace LibraryManagementProject2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //if (!roleManager.RoleExists("Admin"))
            //{
               // var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
               // role.Name = "Admin";
              //  roleManager.Create(role);
                var user = new ApplicationUser();
                
                user.Email = "abc12345@gmail.com";

                string userPWD = "Qwerty@123";
                
                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            //}
            if (!roleManager.RoleExists("User"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
            
        }
    }
}
