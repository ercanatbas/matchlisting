using System;
using MatchList.Domain.Matches;
using MatchList.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MatchList.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        #region .ctor

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        #endregion

        public DbSet<Match>         Matches        { get; set; }
        public DbSet<MatchAuditLog> MatchAuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MatchEntityConfiguration());
            builder.Entity<Match>()
                   .HasIndex(u => u.EventId)
                   .IsUnique();
            builder.Entity<MatchAuditLog>()
                   .HasIndex(u => u.EventId);
        }

        public static void EnsureCreated(IServiceProvider provider)
        {
            var context = provider.GetService<DatabaseContext>();
            context?.Database.Migrate();
        }
    }
}