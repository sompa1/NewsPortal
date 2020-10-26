using Microsoft.EntityFrameworkCore;
using NewsPortal.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Dal
{
    public class NewsPortalDbContext: DbContext
    {
        public NewsPortalDbContext(DbContextOptions options) : base(options) { }
        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Crime"
                },
                new Category
                {
                    Id = 2,
                    Name = "Entertainment"
                },
                new Category
                {
                    Id = 3,
                    Name = "Politics"
                },
                new Category
                {
                    Id = 4,
                    Name = "World News"
                },
                new Category
                {
                    Id = 5,
                    Name = "Impact"
                },
                new Category
                {
                    Id = 6,
                    Name = "Weird News"
                },
                new Category
                {
                    Id = 7,
                    Name = "Black Voices"
                },
                new Category
                {
                    Id = 8,
                    Name = "Women"
                },
                new Category
                {
                    Id = 9,
                    Name = "Comedy"
                },
                new Category
                {
                    Id = 10,
                    Name = "Sports"
                },
                new Category
                {
                    Id = 11,
                    Name = "Business"
                },
                new Category
                {
                    Id = 12,
                    Name = "Tech"
                }

            );
            modelBuilder.Entity<News>().HasData(
                new News
                {
                    Id = 1,
                    Headline = @"There Were 2 Mass Shootings In Texas Last Week, But Only 1 On TV",
                    Author = @"Melissa Jeltsen",
                    ShortDescription = @"She left her husband. He killed their children. Just another day in America.",
                    Body = @"<div><p><span>On the evening of May 15, Amanda and Justin took the kids out for pizza. During the car ride, Amanda told Justin about her new boyfriend, that it was serious and he was planning to move in with her.  </span></p></div>
             <div><p><span> For the past few months, Amanda had been in a long-distance relationship with Seth Richardson, whom she knew from her teen years.They, too, had met through “World of Warcraft,” and had kept in touch for a decade. </span></p></div>
                <div><p><span>“We just had this bond, it never left,” Amanda said. “There was so much positivity when we were together.” </span></p></div>",
                    CategoryId = 1,
                    PublishDate = new DateTime(2018, 5, 26, 7, 0, 0)
                }
            );
        }

    }
}
