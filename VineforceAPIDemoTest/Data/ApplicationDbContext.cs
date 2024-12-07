using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VineforceAPIDemoTest.Models;

namespace VineforceAPIDemoTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
    }
}
