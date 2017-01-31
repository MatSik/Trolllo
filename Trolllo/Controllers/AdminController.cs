using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls.Expressions;
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
        private RoleManager<Role, int> _roleManager;
        
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
        public RoleManager<Role, int> RoleManager
        {
            get
            {
                return _roleManager ?? new RoleManager<Role, int>(new RoleStore<Role, int, UserRole>(_context));
            }
            private set
            {
                _roleManager = value;
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

            var applicationUser = _context.Users.Find(id);

            var a = applicationUser.Roles.First().RoleId;
            
            return RoleManager.Roles.First(x => x.Id == a).Name;
        }


        private void PutAllRolesInViewdata()
        {
            var allRoles = new SelectList(_context.Roles, "Id", "Name");
            ViewData["AllRoles"] = allRoles;
        }

        public PartialViewResult FiltrUsersWithRoles(int selectedRoleFromDropdown)
        {
            var users = UserManager.Users;
            List<UserWithRoleModel> listOfUsers = new List<UserWithRoleModel>();
            var selectedRole = RoleManager.Roles.First(x => x.Id == selectedRoleFromDropdown).Name;
            foreach (var applicationUser in users)
            {
                if (GiveRoleOfOfUser(applicationUser.Id).Equals(selectedRole) || selectedRole.Equals("All"))
                {
                    listOfUsers.Add(CreateUserWithRoleModel(applicationUser));
                }
            }
            return PartialView("_UserAdminPartial", listOfUsers);
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

        private  void RemoveAndAddRole(string oldRole, string newRole, int userId)
        {
             UserManager.RemoveFromRole(userId, oldRole);
             UserManager.AddToRole(userId, newRole);
        }
        [HttpGet]
        public ActionResult AllProjects()
        {
            var allProject = _context.Projects;
            List<ProjectsViewModel> projects = new List<ProjectsViewModel>();
            foreach (var project in allProject)
            {
                projects.Add(CreateProjectsViewModel(project));
            }
            return View(projects);
        }
        private ProjectsViewModel CreateProjectsViewModel(Project project)
        {
            string managerUsername = "";
            if (project.ManagerId != null)
            {
                managerUsername = _context.Users.Find(project.ManagerId).UserName;
            }
            return new ProjectsViewModel
            {
                ManagerId = project.ManagerId,
                Description = project.Description,
                Name = project.Name,
                ProjectId = project.ProjectId,
                TechnologyId = project.TechnologyId,
                TechnologyName = _context.Technologies.Find(project.TechnologyId).Name,
                ManagerUsername = managerUsername,
                AttendCount = SearchAttended(project.ProjectId)
            };
        }
        [HttpGet]
        public ActionResult ManagersAttended(int id)
        {
            var managersAttendedToProject = _context.AttendedToProjects.Where(x => x.ProjectId == id).Include(x => x.ApplicationUser);
            return View(managersAttendedToProject);
        }

        [HttpPost]
        public ActionResult SubmitManager(int managerId, int projectId)
        {
            var projectAndManager = _context.AttendedToProjects.Where(x => x.ProjectId == projectId);
            var project = _context.Projects.Find(projectId);
            project.ManagerId = managerId;
            try
            {
                if (projectAndManager != null)
                {
                    foreach (var deleteAttend in projectAndManager)
                    {
                        _context.AttendedToProjects.Remove(deleteAttend);
                    }
                   
                }
                _context.Entry(project).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("AllProjects");
        }
        private int SearchAttended(int projectId)
        {
            return _context.AttendedToProjects.Count(x => x.ProjectId == projectId);
        }
    }
}