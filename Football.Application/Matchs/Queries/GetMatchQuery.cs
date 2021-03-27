using AutoMapper;
using AutoMapper.QueryableExtensions;
using Football.Application.Commons;
using Football.Domain.MainBoundleContext;
using Football.Domain.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Matchs.Queries
{
    public class GetMatchQuery : IRequest<Response<IList<MatchDto>>>
    {
    }

    public class GetMatchQueryHandler : IRequestHandler<GetMatchQuery, Response<IList<MatchDto>>>
    {
        private IUnitOfWork UnityOfWork { get; }
        private IMapper Mapper { get; }

        public GetMatchQueryHandler(IUnitOfWork unityOfWork, IMapper mapper)
        {
            UnityOfWork = unityOfWork;
            Mapper = mapper;
        }

        public Task<Response<IList<MatchDto>>> Handle(GetMatchQuery request, CancellationToken cancellationToken)
        {
            // All queries done in query handler must not be tracked in order to obtain better performance
            var matchs = UnityOfWork.MatchRepository.Get(true);
            var matchDtos = new List<MatchDto>();
            foreach (var match in matchs)
            {
                var awayTeamManager = UnityOfWork.ManagerRepository.Get(m => m.Id == match.AwayManagerId, null, string.Empty, true).FirstOrDefault();
                var houseTeamManager = UnityOfWork.ManagerRepository.Get(m => m.Id == match.HouseManagerId, null, string.Empty, true).FirstOrDefault();
                var playerMatch = UnityOfWork.PlayerMatchRepository.Get(pm => pm.MatchId == match.Id, null, string.Empty, true);
                var awayPlayerMatchs = playerMatch.OfType<AwayPlayerMatch>().ToList();
                var housePlayerMatchs = playerMatch.OfType<HousePlayerMatch>().ToList();
                var awayPlayers = UnityOfWork.PlayerRepository.Get(true).AsQueryable().Where(ap => awayPlayerMatchs.Any(pm => pm.PlayerId == ap.Id));
                var housePlayers = UnityOfWork.PlayerRepository.Get(true).AsQueryable().Where(hp => awayPlayerMatchs.Any(pm => pm.PlayerId == hp.Id)); 

                matchDtos.Add(new MatchDto
                {
                    Id = match.Id,
                    AwayTeamManager = Mapper.Map<ManagerDto>(awayTeamManager),
                    HouseTeamManager = Mapper.Map<ManagerDto>(houseTeamManager),
                    AwayTeamPlayers = awayPlayers.ProjectTo<PlayerDto>(Mapper.ConfigurationProvider).ToList(),
                    HouseTeamPlayers = housePlayers.ProjectTo<PlayerDto>(Mapper.ConfigurationProvider).ToList(),
                    Referee = Mapper.Map<RefereeDto>(match.Referee),
                    Date = match.Date
                });
            }
            return Task.FromResult(Response.Ok<IList<MatchDto>>(matchDtos, string.Empty));
        }
    }
}
