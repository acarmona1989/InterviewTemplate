using Football.Application.Commons;
using Football.Domain.Extensions;
using Football.Domain.MainBoundleContext;
using Football.Domain.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Matchs.Queries
{
    public class ImproperAligmentOfUpcomingMatchesQuery : IRequest<Response<IList<ImproperAligmentPlayerDto>>>
    {
        public DateTime RequestDatetime { get; set; }
    }

    public class ImproperPlayerOfUpcomingQueryHandler : IRequestHandler<ImproperAligmentOfUpcomingMatchesQuery, Response<IList<ImproperAligmentPlayerDto>>>
    {
        private IUnitOfWork UnityOfWork { get; }

        public ImproperPlayerOfUpcomingQueryHandler(IUnitOfWork unityOfWork)
        {
            UnityOfWork = unityOfWork;
        }

        public Task<Response<IList<ImproperAligmentPlayerDto>>> Handle(ImproperAligmentOfUpcomingMatchesQuery request, CancellationToken cancellationToken)
        {
            // TODO: The api endpoint receive a list of ids. In that case I get all upcoming matches and check all players and managers with one red card or two yellow card.
            // Following the rules of Soccer Manager Football Association. Due the Managers entity is saved in a different table to Player entity, the entity ids could be repeated,
            // I recommend to change the endpoint IncorrectAlignment specifying the entity type in order to avoid confusions. And also I would define the game id as required field
            // of endpoint for allowing simultaneous games
            var matches = UnityOfWork.MatchRepository.Get(m => (m.Date - request.RequestDatetime).TotalMinutes == 5, null, string.Empty, true);

            var improperPlayers = new List<ImproperAligmentPlayerDto>();
            foreach (var match in matches)
            {
                var awayTeamManager = UnityOfWork.ManagerRepository.Get(m => m.Id == match.AwayManagerId && m.RedCard == 1 && m.YellowCard == 2, null, string.Empty, true).FirstOrDefault();
                improperPlayers.Add(new ImproperAligmentPlayerDto { Id = awayTeamManager.Id });

                var houseTeamManager = UnityOfWork.ManagerRepository.Get(m => m.Id == match.HouseManagerId, null, string.Empty, true).FirstOrDefault();
                improperPlayers.Add(new ImproperAligmentPlayerDto { Id = houseTeamManager.Id });

                var playerMatch = UnityOfWork.PlayerMatchRepository.Get(pm => pm.MatchId == match.Id, null, string.Empty, true);
                var awayPlayerMatchs = playerMatch.OfType<AwayPlayerMatch>().ToList();
                var housePlayerMatchs = playerMatch.OfType<HousePlayerMatch>().ToList();
                UnityOfWork.PlayerRepository.Get(true)
                                .Where(ap => awayPlayerMatchs.Any(pm => pm.PlayerId == ap.Id) && ap.RedCard == 1 && ap.YellowCard == 2)
                                .ForEach(ap => improperPlayers.Add(new ImproperAligmentPlayerDto { Id = ap.Id }));
                var housePlayers = UnityOfWork.PlayerRepository.Get(true).AsQueryable()
                                .Where(hp => awayPlayerMatchs.Any(pm => pm.PlayerId == hp.Id && hp.RedCard == 1 && hp.YellowCard == 2))
                                .ForEach(hp => improperPlayers.Add(new ImproperAligmentPlayerDto { Id = hp.Id }));

            }

            return Task.FromResult(Response.Ok<IList<ImproperAligmentPlayerDto>>(improperPlayers, string.Empty));
        }
    }
}
