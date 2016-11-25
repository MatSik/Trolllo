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

        [ForeignKey("Technology")]
        public int TechnologyId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public StateOfProject StateOfProject { get; set; }

        public virtual IEnumerable<WorkTask> WorkTasks { get; set; }

        [ForeignKey("ApplicationUser")]
        public int? ManagerId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }


        public virtual Technology Technology { get; set; }
    }

    public enum StateOfProject
    {
        NotStarted,
        InProgress,
        Suspended,
        Failed,
        Succes
    }
}