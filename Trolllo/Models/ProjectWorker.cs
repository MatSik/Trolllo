using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trolllo.Models
{
    public class ProjectWorker
    {
        [Key]
        public int ProjectWorkerId { get; set; }

        [Index("AddedWorkerAndProject", 1, IsUnique = true)]
        [ForeignKey("ApplicationUser")]
        public int WorkerId { get; set; }
        [Index("AddedWorkerAndProject", 1, IsUnique = true)]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Project Project { get; set; }
    }
}