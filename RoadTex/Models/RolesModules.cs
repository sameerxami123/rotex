using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RoadTex.Models
{
    public class RolesModules
    {
        [Key]
        public int Id { get; set; }
        public IdentityRole Roles { get; set; }
        //public string Roles_Id { get; set; }
        public Modules Modules { get; set; }
        //public int Modules_Id { get; set; }
        public Boolean IsAccess { get; set; } = false;
    }
}