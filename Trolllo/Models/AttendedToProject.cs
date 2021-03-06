﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trolllo.Models
{
    public class AttendedToProject
    {
        [Key]
        public int AttendToProjectId { get; set; }
        [Index("UserAndProject", 1, IsUnique = true)]
        [ForeignKey("ApplicationUser")]
        public int ApplicationUserId { get; set; }
        [Index("UserAndProject", 2, IsUnique = true)]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}