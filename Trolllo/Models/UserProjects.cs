using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trolllo.Models
{
    public class UserProjects
    {
        [ForeignKey("ApplicationUser")]
        public int Id { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
    }
}