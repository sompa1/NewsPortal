using Microsoft.EntityFrameworkCore;
using NewsPortal.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace NewsPortal.Dal {

    public class NewsPortalDbContext : IdentityDbContext<User, IdentityRole<int>, int> {

        public NewsPortalDbContext(DbContextOptions options) : base(options) { }
        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<HomePageContent> HomePageContent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Comment>().HasOne(ent => ent.User).WithMany(ent => ent.Comments).HasForeignKey(ent => ent.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Comment>().HasOne(ent => ent.News).WithMany(ent => ent.Comments).HasForeignKey(ent => ent.NewsId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<News>().HasOne(ent => ent.Author).WithMany(ent => ent.News).HasForeignKey(ent => ent.AuthorId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<HomePageContent>().HasData(
                new HomePageContent { Id = 1, Content = @"Please edit the content of the home page." }
                );
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
