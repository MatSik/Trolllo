using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Trolllo.Models
{
    public class Technology
    {
        [Key]
        public int TechnologyId { get; set; }

        public string Name { get; set; }
    }
}