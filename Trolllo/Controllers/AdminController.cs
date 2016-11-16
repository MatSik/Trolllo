using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Trolllo.Models;

namespace Trolllo.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Admin
        public ActionResult ManageManagers()
        {
            RoleManager<Role, int> roleManager = new RoleManager<Role, int>(new RoleStore<Role, int, UserRole>(_context));
            UserManager<ApplicationUser, int> userManager = new UserManager<ApplicationUser, int>(new UserStore
                <ApplicationUser, Role, int, UserLogin, UserRole, UserClaim>(_context));
            var allRoles = new SelectList(_context.Roles, "Id", "Name");
            ViewData["AllRoles"] = allRoles;
            var listOfUsers = userManager.Users;
            return View(listOfUsers);
        }
    }
}