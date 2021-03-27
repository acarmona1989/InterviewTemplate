using Football.Domain.MainBoundleContext;
using Football.Infrastructure.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Football.Infrastructure.Persistence
{
    public class FootballContext : DbContext
    {
        public FootballContext(DbContextOptions<FootballContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlayerMatchConfiguration());
            modelBuilder.ApplyConfiguration(new MatchConfiguration());
        }

        public DbSet<Manager> Managers { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Referee> Referees { get; set; }
        public DbSet<Match> Matches { get; set; }
    }
}
