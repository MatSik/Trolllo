using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trolllo.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual IEnumerable<WorkTask> WorkTasks { get; set; }

        [ForeignKey("ApplicationUser")]
        public int? ManagerId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}