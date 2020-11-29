using Microsoft.EntityFrameworkCore;
using NewsPortal.Model;
using NewsPortal.Dal.EntityConfigurations;
using NewsPortal.Dal.SeedInterfaces;
using NewsPortal.Dal.SeedServices;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace NewsPortal.Dal
{
    public class NewsPortalDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        private readonly ISeedService _seedService;
        public NewsPortalDbContext(DbContextOptions options, ISeedService seedService) : base(options)
        {
            _seedService = seedService;
        }
        public NewsPortalDbContext(DbContextOptions options) : base(options) { }
        public DbSet<News> News { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<HomePageContent> HomePageContent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Comment>().HasOne(ent => ent.User).WithMany(ent => ent.Comments).HasForeignKey(ent => ent.UserId).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<News>().HasOne(ent => ent.Author).WithMany(ent => ent.News).HasForeignKey(ent => ent.AuthorId).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<HomePageContent>().HasData(
                new HomePageContent { Id = 1, Content = @"Please edit the content of the home page." }
                );
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration(_seedService));
            modelBuilder.ApplyConfiguration(new NewsEntityConfiguration(_seedService));
        }

    }
}
