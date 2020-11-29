using NewsPortal.Model;
using NewsPortal.Dal.SeedInterfaces;
using NewsPortal.Dal.SeedServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Dal.EntityConfigurations
{
    public class NewsEntityConfiguration : IEntityTypeConfiguration<News>
    {
        private readonly ISeedService _seedService;

        public NewsEntityConfiguration(ISeedService seedService)
        {
            _seedService = seedService;
        }

        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.HasData(_seedService.News);
        }
    }
}
