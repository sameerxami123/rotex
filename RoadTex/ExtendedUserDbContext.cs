using Microsoft.AspNet.Identity.EntityFramework;
using RoadTex.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Web;

namespace RoadTex
{
    public class ExtendedUserDbContext:IdentityDbContext
    {
        public DbSet<RolesModules> RolesModules { get; set; }
        public DbSet<Modules> Modules { get; set; }
        public virtual DbSet<Alert> Alerts { get; set; }
        public virtual DbSet<CallHistory> CallHistories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public virtual DbSet<CustomerPersonalInfo> CustomerPersonalInfoes { get; set; }
        public virtual DbSet<FollowUp> FollowUps { get; set; }
        public virtual DbSet<FutureAppointment> FutureAppointments { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<OfferSale> OfferSales { get; set; }
        public virtual DbSet<Reminder> Reminders { get; set; }
        //public System.Data.Entity.DbSet<AspNetWebApplication.Models.RegisterModel> RegisterModels { get; set; }

        public ExtendedUserDbContext() : base("name=Roadtex8Entities")
        {
           
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

         
            var user = modelBuilder.Entity<ExtendedUser>();
            user.Property(x => x.Email).IsRequired().HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("FullNameIndex")));

        }

        public System.Data.Entity.DbSet<RoadTex.ExtendedUser> IdentityUsers { get; set; }
    }
}