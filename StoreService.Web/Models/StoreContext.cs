using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StoreService.Web.Models
{
    public partial class StoreContext : DbContext
    {
        public StoreContext()
        {
        }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<ProgramBook> ProgramBooks { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<ProgramListing> ProgramListings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.HasIndex(e => e.OrdersId);
            });

            SeedData(modelBuilder);
        }

        protected void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>().HasData(
                new Topic { TopicId = 1, TopicName = "Engineering" },
                new Topic { TopicId = 2, TopicName = "Games" },
                new Topic { TopicId = 3, TopicName = "Mathematics" },
                new Topic { TopicId = 4, TopicName = "Navigation" }
            );
        }
    }
}
