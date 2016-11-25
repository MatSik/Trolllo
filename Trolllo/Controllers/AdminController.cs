using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Schema;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Trolllo.Constans;
using Trolllo.Models;

namespace Trolllo.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;
        
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        private ApplicationDbContext _context = new ApplicationDbContext();




        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ManageManagers()
        {
            PutAllRolesInViewdata();
            
            var users = UserManager.Users;
            List<UserWithRoleModel> listOfUsers = new List<UserWithRoleModel>();
            foreach (var applicationUser in users)
            {
                listOfUsers.Add(CreateUserWithRoleModel(applicationUser));
            }
            return View(listOfUsers);
        }

        private UserWithRoleModel CreateUserWithRoleModel(ApplicationUser applicationUser)
        {
            return new UserWithRoleModel
            {
                Id = applicationUser.Id,
                Name = applicationUser.Name,
                Surname = applicationUser.Surname,
                Birthday = applicationUser.Birthday,
                Email = applicationUser.Email,
                EmailConfirmed = applicationUser.EmailConfirmed,
                PhoneNumber = applicationUser.PhoneNumber,
                PhoneNumberConfirmed = applicationUser.PhoneNumberConfirmed,
                UserName = applicationUser.UserName,
                Role = GiveRoleOfOfUser(applicationUser.Id)
            };
        }

        private string GiveRoleOfOfUser(int id)
        {
            RoleManager<Role,int> roleManager = new RoleManager<Role, int>(new RoleStore<Role, int, UserRole>(_context));

            var applicationUser = _context.Users.Find(id);

            var a = applicationUser.Roles.First().RoleId;
            
            return roleManager.Roles.First(x => x.Id == a).Name;
        }

        private void PutAllRolesInViewdata()
        {
            var allRoles = new SelectList(_context.Roles, "Id", "Name");
            ViewData["AllRoles"] = allRoles;
        }

        [HttpGet]
        public ActionResult PromoteOrDegrade(int id, bool promote)
        {
            var applicationUser = _context.Users.Find(id);
            var userWithRoleModel = CreateUserWithRoleModel(applicationUser);
            ViewData["Promote"] = promote;
            return View(userWithRoleModel);
        }

        [HttpPost, ActionName("PromoteOrDegrade")]
        [ValidateAntiForgeryToken]
        public ActionResult PromoteOrDegardePerson(int id, bool promote)
        {
            if (promote)
            {
                Promote(id);
            }
            else
            {
                Degarde(id);
            }
            
            return RedirectToAction("ManageManagers");
        }

        private void Promote(int id)
        {
            var userRole = GiveRoleOfOfUser(id);
            switch (userRole)
            {
                case Constant.USER:
                    RemoveAndAddRole(userRole, Constant.MANAGER, id);
                    break;
                case Constant.MANAGER:
                    RemoveAndAddRole(userRole, Constant.ADMIN, id);
                    break;
            }
        }

        private void Degarde(int id)
        {
            var userRole = GiveRoleOfOfUser(id);
            switch (userRole)
            {
                case Constant.MANAGER:
                    RemoveAndAddRole(userRole, Constant.USER, id);
                    break;
                case Constant.ADMIN:
                    RemoveAndAddRole(userRole, Constant.MANAGER, id);
                    break;
            }
        }

        private async void RemoveAndAddRole(string oldRole, string newRole, int userId)
        {
            await UserManager.RemoveFromRoleAsync(userId, oldRole);
            await UserManager.AddToRoleAsync(userId, newRole);
        }
    }
}