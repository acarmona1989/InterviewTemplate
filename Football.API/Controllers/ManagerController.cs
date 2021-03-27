using Football.Domain.MainBoundleContext;
using Football.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Football.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        // TODO: Change the DBContext dependency for a service abstraction, which will provide the way of handling entity.
        // Using this approach the api controller will be a façade publishing the Domain and Application layers.
        // I recommend to use Mediator pattern, which it enables controllers to communicate to application services, without knowing each services abstraction
        readonly FootballContext footballContext;
        public ManagerController(FootballContext footballContext)
        {
            this.footballContext = footballContext;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<Manager>> Get()
        {
            return this.Ok(footballContext.Managers);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            var response = footballContext.Managers.Find(id);
            if (response == default)
                this.NotFound();
            return this.Ok();
        }

        [HttpPost]
        public ActionResult Post(Manager manager)
        {
            var response = footballContext.Managers.Add(manager).Entity;
            return this.CreatedAtAction(nameof(Get), response.Id, response);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id, Manager manager)
        {
            if (footballContext.Managers.Find(id) == default)
                return this.NotFound();

            footballContext.Managers.Update(manager);           
            return this.Ok();
        }
    }
}
