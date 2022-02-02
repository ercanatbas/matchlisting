using MatchList.Domain.Matches;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchList.Infrastructure.Configurations
{
    public class MatchEntityConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.Property(c => c.Country).HasColumnType("varchar(100)").HasMaxLength(100).IsRequired();
            builder.Property(c => c.League).HasColumnType("varchar(150)").HasMaxLength(150).IsRequired();
            builder.Property(c => c.HomeTeam).HasColumnType("varchar(100)").HasMaxLength(100).IsRequired();
            builder.Property(c => c.AwayTeam).HasColumnType("varchar(100)").HasMaxLength(100).IsRequired();
        }
    }
}