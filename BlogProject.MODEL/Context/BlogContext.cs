using BlogProject.CORE.Entity;
using BlogProject.MODEL.Entities;
using BlogProject.MODEL.Maps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BlogProject.MODEL.Context
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new UserMap());

            // Silmiyoruz
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Sql Server Bağlantı cümlesi - Connection String");

        //    base.OnConfiguring(optionsBuilder);
        //}

        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Added).ToList();

            string computerName = Environment.MachineName;
            string ipAddress = "127.0.0.1";
            DateTime date = DateTime.Now;

            foreach (var item in modifiedEntries)
            {
                CoreEntity entity = item.Entity as CoreEntity;

                if (item != null)
                {
                    switch (item.State)
                    {
                        case EntityState.Modified:
                            entity.UpdatedComputerName = computerName;
                            entity.UpdatedIP = ipAddress;
                            entity.UpdatedDate = date;
                            break;
                        case EntityState.Added:
                            entity.CreatedComputerName = computerName;
                            entity.CreatedIP = ipAddress;
                            entity.CreatedDate = date;
                            break;
                        default:
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
