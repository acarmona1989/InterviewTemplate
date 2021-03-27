using Football.Application.Commons;
using Football.Domain.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Statistics.Queries
{
    public class GetMinutesPlayedQuery: IRequest<Response<IList<MinutesPlayedDto>>>
    {
        
    }

    public class GetMinutesPlayedQueryHandler : IRequestHandler<GetMinutesPlayedQuery, Response<IList<MinutesPlayedDto>>>
    {
        private IUnitOfWork UnityOfWork { get; }

        public GetMinutesPlayedQueryHandler(IUnitOfWork unityOfWork)
        {
            UnityOfWork = unityOfWork;
        }

        public Task<Response<IList<MinutesPlayedDto>>> Handle(GetMinutesPlayedQuery request, CancellationToken cancellationToken)
        {
            var minutesPlayed = new List<MinutesPlayedDto>();
            var players = UnityOfWork.PlayerRepository.Get(true).AsQueryable();
            foreach (var player in players)
            {
                minutesPlayed.Add(new MinutesPlayedDto
                {
                    Id = player.Id,
                    Name = player.Name,
                    Total = player.MinutesPlayed
                });
            }

            return Task.FromResult(Response.Ok<IList<MinutesPlayedDto>>(minutesPlayed, string.Empty));
        }
    }
}
