using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadTex
{
    public class ExtendedUser:IdentityUser
    {
        public override string Id { get => base.Id; set => base.Id = value; }
        public override string UserName { get => base.UserName; set => base.UserName = value; }
        public override string Email { get => base.Email; set => base.Email = value; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public bool IsSalesRep { get; set; }
        public bool IsPreprer { get; set; }

     
    }
}