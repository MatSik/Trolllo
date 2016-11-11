using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trolllo.Models
{
    public class WorkTask
    {
        [Key]
        public int WorkTaskId { get; set; }

        public string Task { get; set; }

        public string TaskDescription { get; set; }

        public State State { get; set; }

        public int Difficulty { get; set; }

        [ForeignKey("ApplicationUser")]
        public int? PerformerId { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public enum State
    {
        NotAssigned,
        InProgress,
        Canceled,
        Completed,
        Failed
    }
}