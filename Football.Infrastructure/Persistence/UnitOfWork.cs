using Football.Domain.MainBoundleContext;
using Football.Domain.Persistence;
using System.Linq;

namespace Football.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        internal FootballContext context;
        public IRepository<Manager> ManagerRepository { get; }
        public IRepository<Match> MatchRepository { get; }
        public IRepository<Referee> RefereeRepository { get; }
        public IRepository<Player> PlayerRepository { get; }
        public IRepository<PlayerMatch> PlayerMatchRepository { get; }

        public UnitOfWork(
            FootballContext context,
            IRepository<Match> matchRepository,
            IRepository<Manager> managerRepository,
            IRepository<Referee> refereeRepository,
            IRepository<Player> playerRepository,
            IRepository<PlayerMatch> playerMatchRepository)
        {
            MatchRepository = matchRepository;
            this.context = context;
            ManagerRepository = managerRepository;
            RefereeRepository = refereeRepository;
            PlayerRepository = playerRepository;
            PlayerMatchRepository = playerMatchRepository;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Rollback()
        {
            context
                .ChangeTracker
                .Entries()
                .ToList()
                .ForEach(x => x.Reload());
        }
    }
}
