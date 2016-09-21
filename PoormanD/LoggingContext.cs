using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PoormanD.Models;

namespace PoormanD
{
    public class LoggingContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var existingExtention = optionsBuilder.Options.FindExtension<CustomSqliteOptionsExtension>();
            var newExtention = existingExtention == null
                ? new CustomSqliteOptionsExtension()
                : new CustomSqliteOptionsExtension(existingExtention);
            newExtention.ConnectionString = "Filename=./log.sqlite";
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(newExtention);
            optionsBuilder.ConfigureWarnings(
                    w => w.Configuration.TryAddExplicit(
                        RelationalEventId.AmbientTransactionWarning,
                        WarningBehavior.Throw
                    )
            );
        }
    }
}