using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroopWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GroopWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Race> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}