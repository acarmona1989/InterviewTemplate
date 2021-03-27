using Football.Application.Commons;
using Football.Application.Matchs.Commands;
using Football.Application.Matchs.Queries;
using Football.Domain.MainBoundleContext;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Football.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private IMediator Mediator { get; }

        public MatchController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public Task<Response<IList<MatchDto>>> Get()
        {
            return Mediator.Send(new GetMatchQuery());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            //var response = footballContext.Matches.Find(id);
            //if (response == default)
            //    this.NotFound();
            return this.Ok();
        }

        [HttpPost]
        public async Task<Response<int>> Post([FromBody] CreateMatchCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id, Match match)
        {
            //if (footballContext.Matches.Find(id) == default)
            //    return this.NotFound();

            //footballContext.Matches.Update(match);
            return this.Ok();
        }
    }
}
