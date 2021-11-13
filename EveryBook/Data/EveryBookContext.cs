using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EveryBook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EveryBook.Data
{
    public class EveryBookContext : IdentityDbContext<ExtendUser>
    {
        public DbSet<Genre> Genre { get; set; }

        public DbSet<Book> Book { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Bug> Bug { get; set; }

        public DbSet<Location> Location { get; set; }

        public DbSet<DistributionUnit> DistributionUnit { get; set; }

        public DbSet<ExtendUser> ExtendUser { get; set; }

        public EveryBookContext (DbContextOptions<EveryBookContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
