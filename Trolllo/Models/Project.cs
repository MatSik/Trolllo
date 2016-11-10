﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public IList<WorkTask> WorkTasks { get; set; }

        public int? Id { get; set; }

        public IList<ApplicationUser> ProjectWorkers { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}