using Football.Domain.MainBoundleContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football.Infrastructure.Persistence.EntityConfiguration
{
    public class PlayerMatchConfiguration : IEntityTypeConfiguration<PlayerMatch>
    {
        public void Configure(EntityTypeBuilder<PlayerMatch> builder)
        {
            builder.HasDiscriminator(pm => pm.Type)
                   .HasValue<AwayPlayerMatch>("AwayPlayer")
                   .HasValue<HousePlayerMatch>("HousePlayer");

            builder
                   .HasOne(pm => pm.Match)
                   .WithMany(m => m.PlayersMatch)
                   .HasForeignKey(pm => pm.MatchId)
                   .HasPrincipalKey(m => m.Id);

            builder
                   .HasOne(pm => pm.Player)
                   .WithMany(p => p.PlayerMatchs)
                   .HasForeignKey(pm => pm.PlayerId)
                   .HasPrincipalKey(m => m.Id);
        }
    }
}
