
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Trolllo.Models;


[assembly: OwinStartupAttribute(typeof(Trolllo.Startup))]

namespace Trolllo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           ConfigureAuth(app);
            AddRoles();

        }

        public void AddRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<Role, int>(new RoleStore<Role, int, UserRole>(context));
            var UserManager = new UserManager<ApplicationUser, int>(new UserStore
                <ApplicationUser, Role, int, UserLogin, UserRole, UserClaim>(context));

            AddAdminRole(roleManager, UserManager);
            AddManager(roleManager);
            AddUserRole(roleManager);
            
        }

        private void AddUserRole(RoleManager<Role, int> roleManager)
        {
            if (!roleManager.RoleExists("User"))
            {
                var role = new Role()
                {
                    Name = "User"
                };

                roleManager.Create(role);
            }
        }


        private void AddManager(RoleManager<Role, int> roleManager)
        {
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new Role()
                {
                    Name = "Manager"
                };

                roleManager.Create(role);
            }
        }

        private void AddAdminRole(RoleManager<Role, int> roleManager, UserManager<ApplicationUser, int> UserManager)
        {
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Role()
                {
                    Name = "Admin"
                };

                roleManager.Create(role);
                AddAdminUser(UserManager);
            }
            
        }

        private void AddAdminUser(UserManager<ApplicationUser,int> UserManager)
        {

            var user = new ApplicationUser()
            {
                UserName = "Mateusz.Sikora.95@wp.pl",
                Name = "Jakub",
                Surname = "Sikora",
                Birthday = new DateTime(1995, 03, 26),
                Email = "Mateusz.Sikora.95@wp.pl",
            };
            var password = "!Qaz2wsx";

            var checkUser = UserManager.Create(user, password);

            if (checkUser.Succeeded)
            {
                UserManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
