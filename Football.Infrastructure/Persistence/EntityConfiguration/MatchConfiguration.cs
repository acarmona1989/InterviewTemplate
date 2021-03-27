using Football.Domain.MainBoundleContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football.Infrastructure.Persistence.EntityConfiguration
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.Ignore(m => m.AwayPlayersMatch)
                   .Ignore(m => m.HousePlayersMatch);
        }
    }
}
