using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;
using WebApplication1.Data;
using WebApplication1.Entities;

namespace WebApplication1.Data
{
    public class InsuranceContext : DbContext
    {
        public DbSet<InsuranceCoverage> insuranceCoverages;
        public DbSet<ClientRequest> clientRequests;
        public DbSet<TotalClientRequest> totalClientRequests;

        public InsuranceContext(DbContextOptions<InsuranceContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TotalClientRequest>()
            .Property(e => e.CoverageRequests)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<TotalClientRequest.CoverageRequest>>(v, (JsonSerializerOptions)null)
            );

            modelBuilder.Entity<InsuranceCoverage>().HasData(
                new InsuranceCoverage { Id = 1, Type = "Surgery", MinimumAmount = 5000, MaximumAmount = 500000000 },
                new InsuranceCoverage { Id = 2, Type = "Dentistry", MinimumAmount = 4000, MaximumAmount = 400000000 },
                new InsuranceCoverage { Id = 3, Type = "Hospitalization", MinimumAmount = 2000, MaximumAmount = 200000000 }
            );
        }
    }
}