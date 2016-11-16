using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trolllo.Models
{
    public class AskForCommitToProject
    {
        [Key]
        public int AskForCommitToProjectId { get; set; }

        [Index("CommitWorkerAndProject", 1, IsUnique = true)]
        [ForeignKey("ApplicationUser")]
        public int WorkerId { get; set; }
        [Index("CommitWorkerAndProject", 2, IsUnique = true)]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Project Project { get; set; }
    }
}