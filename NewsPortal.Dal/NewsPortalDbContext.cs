﻿using Microsoft.EntityFrameworkCore;
using NewsPortal.Dal.Entities;
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
    public class NewsPortalDbContext: IdentityDbContext<User, IdentityRole<int>, int>
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.ApplyConfiguration(new AuthorEntityConfiguration(_seedService));
            modelBuilder.ApplyConfiguration(new NewsEntityConfiguration(_seedService));
            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration(_seedService));


            /*
            modelBuilder.Entity<News>().HasData(
                new News
                {
                    Id = 1,
                    Headline = @"There Were 2 Mass Shootings In Texas Last Week, But Only 1 On TV",
                    AuthorId = 1,
                    ShortDescription = @"She left her husband. He killed their children. Just another day in America.",
                    Body = @"<div><p><span>On the evening of May 15, Amanda and Justin took the kids out for pizza. During the car ride, Amanda told Justin about her new boyfriend, that it was serious and he was planning to move in with her.  </span></p></div>
             <div><p><span> For the past few months, Amanda had been in a long-distance relationship with Seth Richardson, whom she knew from her teen years.They, too, had met through “World of Warcraft,” and had kept in touch for a decade. </span></p></div>
                <div><p><span>“We just had this bond, it never left,” Amanda said. “There was so much positivity when we were together.” </span></p></div>",
                    CategoryId = 1,
                    PublishDate = new DateTime(2018, 5, 26, 7, 0, 0)
                },
                new News
                {
                    Id = 2,
                    Headline = @"Will Smith Joins Diplo And Nicky Jam For The 2018 World Cup's Official Song",
                    AuthorId = 2,
                    ShortDescription = @"Of course it has a song.",
                    Body = @"<div><p>The <a href=""https://www.huffpost.com/topic/fifa-world-cup"">2018 FIFA World Cup</a> starts June 14 in Russia, and now it has an official song. </p></div>
<div><p>Producer Diplo and reggaeton star Nicky Jam collaborate on “Live It Up,” which also features Albanian singer Era Istrefi and actor Will Smith, who is trying to<a href=""https://youtu.be/6wGj89GE6aQ""> restart his music career</a>.</p></div>
<div><p>Traditionally, over the last two decades, each World Cup has had an official song.The song for the 2010 World Cup in South Africa was<a href=""https://www.youtube.com/watch?v=pRpeEdMmmQ0"">“Waka Waka” by Shakira</a>. For the 2014 World Cup in Brazil, the song was<a href=""https://www.youtube.com/watch?v=9W3sWiZ-iO8"">“We Are One (Ole Ola)” by Pitbull</a>.  </p></div>",
                    CategoryId = 2,
                    PublishDate = new DateTime(2018, 5, 26, 8, 0, 0)
                },
                new News
                {
                    Id = 3,
                    Headline = @"With Its Way Of Life At Risk, This Remote Oyster-Growing Region Called In Robots",
                    AuthorId = 3,
                    ShortDescription = @"The revolution is coming to rural New Brunswick.",
                    Body = @"<div><p><span>NEGUAC, Canada ― When the harbors aren’t frozen, Maxime Daigle and his older brother Jean-Francois often take to the cold waters off New Brunswick in one of their family’s flat-bottomed boats to harvest oysters. The siblings are among hundreds of producers along this rocky coast who grow oysters for La Maison BeauSoleil in the company’s distinctive floating bags. </span></p></div>
             < div><p>For the Daigles this is a family business.Maxime’s father, Maurice, co-owns La Maison BeauSoleil, an oyster grower, packer and distributor that he runs with his business partner, Amédée Savoie, whose son, Allain Savoie, also works in the company.</p></div>
<div><p><span> When the business started growing and selling oysters from the coastal village of Neguac, at the southern end of the Acadian Peninsula, each wooden box was painstakingly packed by hand. </span></p></div>
<div><p><span> That was 18 years ago.La Maison BeauSoleil ― </span>called simply BeauSoleil within the seafood business ― <span> has since become Canada’s largest producer of cocktail-sized oysters. Demand for its tasty, teardrop-shaped product is at an all-time high. While the company is working hard to keep up, business as usual isn’t cutting it.</span> </p></div>
<div><p><span> So a new generation of robots is coming.</span></p></div>
<div><p><span> While details are still closely guarded, the company is developing a state-of-the-art automated sorting and packing line that will revolutionize its business.Software-controlled robots,</span> <span> able to mimic the small, complicated movements of human oyster pickers</span><strong>, </strong><span> will give the company a new competitive edge, says Savoie, and allow them to double production. </span></p></div>
<div><p><span> This is the type of investment and technology that has often been missing from this country’s traditional fishing economy.Many of Atlantic Canada’s rural coastal communities have been treading water since<a href=""http://www.cbc.ca/news/canada/remembering-the-mighty-cod-fishery-20-years-after-moratorium-1.1214172""> overfishing caused cod to collapse</a> in the 1990s, and falling further behind could signal the end for the region’s historic way of life. </span></p></div>
<div><p><span> But this is a family business, and the co-owners are trying to build a legacy for their children in this remote region.</span></p></div>",
                    CategoryId = 5,
                    PublishDate = new DateTime(2018, 5, 27, 8, 0, 0)
                }
            ); */
        }

    }
}
