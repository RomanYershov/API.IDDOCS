using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    class DocConfiguration : IEntityTypeConfiguration<IdDoc>
    {
        public void Configure(EntityTypeBuilder<IdDoc> builder)
        {
            
        }
    }
}
