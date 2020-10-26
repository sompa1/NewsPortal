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
    }
}
