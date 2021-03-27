using Football.Application.Commons;
using Football.Domain.MainBoundleContext;
using Football.Domain.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Matchs.Commands
{
    public class CreateMatchCommand : IRequest<Response<int>>
    {
        public int HouseManager { get; set; }
        public int AwayManager { get; set; }
        public int Referee { get; set; }
        public IEnumerable<int> HouseTeam { get; set; }
        public IEnumerable<int> AwayTeam { get; set; }
        public DateTime Date { get; set; }
    }

    public class CreateMatchCommandHandler : IRequestHandler<CreateMatchCommand, Response<int>>
    {
        protected IUnitOfWork UnitOfWork { get; }

        public CreateMatchCommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        private void InsertPlayersMatch(IEnumerable<PlayerMatch> playerMatches)
        {
            foreach (var item in playerMatches)
            {
                UnitOfWork.PlayerMatchRepository.Insert(item);
            }
        }

        public Task<Response<int>> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            var awayManager = UnitOfWork.ManagerRepository.GetByID(request.AwayManager);
            var houseManager = UnitOfWork.ManagerRepository.GetByID(request.HouseManager);
            var refere = UnitOfWork.RefereeRepository.GetByID(request.Referee);

            var match = new Match
            {
                AwayManager = awayManager,
                HouseManager = houseManager,
                Referee = refere,
                Date = request.Date
            };

            try
            {
                match = UnitOfWork.MatchRepository.InsertAtomic(match);
                var awayMatchPlayers = UnitOfWork.PlayerRepository.Get(p => request.AwayTeam.Any(item => item == p.Id))
                                        .Select(player => new AwayPlayerMatch
                                        {
                                            Player = player,
                                            Match = match,
                                            Type = PlayerMatchType.AwayPlayer
                                        });


                var houseMatchPlayers = UnitOfWork.PlayerRepository.Get(p => request.HouseTeam.Any(item => item == p.Id))
                                        .Select(player => new HousePlayerMatch
                                        {
                                            Player = player,
                                            Match = match,
                                            Type = PlayerMatchType.HousePlayer
                                        });

                InsertPlayersMatch(awayMatchPlayers);
                InsertPlayersMatch(houseMatchPlayers);

                UnitOfWork.Commit();
                return Task.FromResult(Response.Ok(match.Id, "Match Created"));

            }
            catch (Exception e)
            {
                UnitOfWork.Rollback();
                return Task.FromResult(Response.Fail<int>(e.Message));
            }
        }
    }
}
