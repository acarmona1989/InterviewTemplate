using Football.Domain.MainBoundleContext;

namespace Football.Domain.Persistence
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
        IRepository<Manager> ManagerRepository { get; }
        IRepository<Match> MatchRepository { get; }
        IRepository<Referee> RefereeRepository { get; }
        IRepository<Player> PlayerRepository { get; }
        IRepository<PlayerMatch> PlayerMatchRepository { get; }
    }
}
