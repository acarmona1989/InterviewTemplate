using Football.Application.Commons;
using Football.Domain.MainBoundleContext;
using Football.Domain.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Statistics.Queries
{
    public class GetCardsQuery : IRequest<Response<IList<CardDto>>>
    {
        public string CardType { get; set; }
    }

    public static class CardType
    {
        public static string Yellow = "Yellow";
        public static string Red = "Red";
    }


    public class GetYellowCardsQueryHandler : IRequestHandler<GetCardsQuery, Response<IList<CardDto>>>
    {
        private IUnitOfWork UnityOfWork { get; }

        public GetYellowCardsQueryHandler(IUnitOfWork unityOfWork)
        {
            UnityOfWork = unityOfWork;
        }

        public Task<Response<IList<CardDto>>> Handle(GetCardsQuery request, CancellationToken cancellationToken)
        {
            var yellowCards = new List<CardDto>();

            Expression<Func<Player, bool>> filterPlayer = null;
            Expression<Func<Manager, bool>> filterManager = null;
            if (request.CardType == CardType.Yellow)
            {
                filterPlayer = p => p.YellowCard > 0;
                filterManager = m => m.YellowCard > 0;
            }
            else
            {
                filterPlayer = p => p.RedCard > 0;
                filterManager = m => m.RedCard > 0;
            }

            var players = UnityOfWork.PlayerRepository.Get(filterPlayer, null, string.Empty, true).AsQueryable();
            foreach (var player in players)
            {
                yellowCards.Add(new CardDto
                {
                    Id = player.Id,
                    Name = player.Name,
                    Total = player.YellowCard
                });
            }

            var managers = UnityOfWork.ManagerRepository.Get(filterManager, null, string.Empty, true).AsQueryable();
            foreach (var manager in managers)
            {
                yellowCards.Add(new CardDto
                {
                    Id = manager.Id,
                    Name = manager.Name,
                    Total = manager.YellowCard
                });
            }
            return Task.FromResult(Response.Ok<IList<CardDto>>(yellowCards, string.Empty));
        }
    }
}
