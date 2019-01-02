using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadTex.Models
{
    public class ExtendedRole:IdentityRole
    {
        public ExtendedRole()
        {
            this.Modules = new List<Modules>();
        }
        public List<Modules> Modules { get; set; }

    }
}