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
    public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
    {
        private readonly ISeedService _seedService;

        public AuthorEntityConfiguration(ISeedService seedService)
        {
            _seedService = seedService;
        }

        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(_seedService.Authors.Values);
        }
    }
}
