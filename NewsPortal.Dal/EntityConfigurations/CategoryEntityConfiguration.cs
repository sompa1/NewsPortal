using NewsPortal.Dal.Entities;
using NewsPortal.Dal.SeedInterfaces;
using NewsPortal.Dal.SeedServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Dal.EntityConfigurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        private readonly ISeedService _seedService;

        public CategoryEntityConfiguration(ISeedService seedService)
        {
            _seedService = seedService;
        }

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(_seedService.Categories.Values);
        }
    }
}
