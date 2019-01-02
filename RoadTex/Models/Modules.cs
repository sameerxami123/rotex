using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoadTex.Models
{
    public class Modules
    {
        public Modules()
        {
            this.ExtendedRoles = new List<ExtendedRole>();
        }
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public List<ExtendedRole> ExtendedRoles { get; set; }
    }
}