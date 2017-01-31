using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trolllo.Models
{
    public class UserWithRoleModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Birthday { get; set; }

        public string UserName { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool EmailConfirmed { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }

    public class ProjectsViewModel
    {
        public int ProjectId { get; set; }

        public int TechnologyId { get; set; }

        public string TechnologyName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ManagerId { get; set; }

        public string ManagerUsername { get; set; }

        public int AttendCount { get; set; }
    }
}