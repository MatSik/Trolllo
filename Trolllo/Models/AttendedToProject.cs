using System;
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

        [ForeignKey("ApplicationUser")]
        public int Id { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}