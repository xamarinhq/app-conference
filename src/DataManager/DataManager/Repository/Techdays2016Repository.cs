using DataManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DataManager.Repository
{
    public class Techdays2016Repository : DbContext
    {
        public Techdays2016Repository() : base("Techdays2016Connection")//base("Techdays2016ConnectionLocalDB")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Category> Categories{get;set;}
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<FeaturedEvent> FeaturedEvents { get; set; }
        public DbSet<Feedback> Featback{ get;set; }
        public DbSet<MiniHack> MiniHacks { get; set;}
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<SponsorLevel> SponsorLevels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

    }
}