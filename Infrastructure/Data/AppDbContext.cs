using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {   
        public DbSet<IdDoc> Docs { get; set; }  
        public DbSet<CacheFile> Files { get; set; }


        public AppDbContext(DbContextOptions options) : base(options) { }
    }
}
