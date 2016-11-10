using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public int? Id { get; set; }

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