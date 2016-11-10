using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trolllo.Models
{
    public class WorkTask
    {
        public int WorkTaskId { get; set; }

        public string Task { get; set; }

        public string TaskDescription { get; set; }

        public State State { get; set; }

        public int Difficulty { get; set; }
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