using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "AuthorDb");
        }



        public DbSet<IdDoc> Docs { get; set; }  
        public DbSet<CacheFile> Files { get; set; }
        public DbSet<Client> Clients { get; set; }


        //public AppDbContext(DbContextOptions options) : base(options) { }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
