using Microsoft.EntityFrameworkCore;
using IpLookup.Api.Models.Entities;

namespace IpLookup.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<IpQuery> IpQueries { get; set; }
    }
}
