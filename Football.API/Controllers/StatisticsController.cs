using Football.Application.Commons;
using Football.Application.Statistics.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Football.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private IMediator Mediator { get; }

        public StatisticsController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet]
        [Route("yellowcards")]
        public Task<Response<IList<CardDto>>> GetYellowCards()
        {
            return Mediator.Send(new GetCardsQuery { 
                CardType = CardType.Yellow
            });
        }

        [HttpGet]
        [Route("redcards")]
        public Task<Response<IList<CardDto>>> GetRedCards()
        {
            return Mediator.Send(new GetCardsQuery
            {
                CardType = CardType.Red
            });
        }

        [HttpGet]
        [Route("minutesplayed")]
        public Task<Response<IList<MinutesPlayedDto>>> GetMinutesPlayed()
        {
            return Mediator.Send(new GetMinutesPlayedQuery());
        }
    }
}
