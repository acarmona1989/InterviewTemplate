using Football.Application.Matchs.Queries;
using Football.Domain.Extensions;
using Football.Domain.MainBoundleContext;
using Football.Domain.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Football.Application.ExternalServices
{
    public class ImproperAligmentNotificationService : IImproperAligmentNotificationService
    {
        private const string URL = "http://http://interview-api.azurewebsites.net/";
        private const string ENDPOINT = "/api/IncorrectAlignment";
        private IUnitOfWork UnitOfWork { get; }

        public ImproperAligmentNotificationService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task NotifyImpromerAligmentAsync()
        {
            var improperAligment = GetImproperAliment();

            if (improperAligment.Any())
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(URL)
                };
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                var myContent = JsonConvert.SerializeObject(improperAligment);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                var result = await client.PostAsync(ENDPOINT, byteContent);

                //TODO: Implement retry pattern here and error handler
            }
        }

        private List<ImproperAligmentPlayerDto> GetImproperAliment()
        {
            // TODO: The api endpoint receive a list of ids. In that case I get all upcoming matches and check all players and managers with one red card or two yellow card.
            // Following the rules of Soccer Manager Football Association. Due the Managers entity is saved in a different table to Player entity, the entity ids could be repeated,
            // I recommend to change the endpoint IncorrectAlignment specifying the entity type in order to avoid confusions. And also I would define the game id as required field
            // of endpoint for allowing simultaneous games

            var improperPlayers = new List<ImproperAligmentPlayerDto>();
            var matches = UnitOfWork.MatchRepository.Get(true).Where(m => (m.Date - DateTime.Now).TotalMinutes == 5);

            foreach (var match in matches)
            {
                var awayTeamManager = UnitOfWork.ManagerRepository.Get(m => m.Id == match.AwayManagerId && m.RedCard == 1 && m.YellowCard == 2, null, string.Empty, true).FirstOrDefault();
                improperPlayers.Add(new ImproperAligmentPlayerDto { Id = awayTeamManager.Id });

                var houseTeamManager = UnitOfWork.ManagerRepository.Get(m => m.Id == match.HouseManagerId, null, string.Empty, true).FirstOrDefault();
                improperPlayers.Add(new ImproperAligmentPlayerDto { Id = houseTeamManager.Id });

                var playerMatch = UnitOfWork.PlayerMatchRepository.Get(pm => pm.MatchId == match.Id, null, string.Empty, true);
                var awayPlayerMatchs = playerMatch.OfType<AwayPlayerMatch>().ToList();
                var housePlayerMatchs = playerMatch.OfType<HousePlayerMatch>().ToList();
                UnitOfWork.PlayerRepository.Get(true)
                                .Where(ap => awayPlayerMatchs.Any(pm => pm.PlayerId == ap.Id) && ap.RedCard == 1 && ap.YellowCard == 2)
                                .ForEach(ap => improperPlayers.Add(new ImproperAligmentPlayerDto { Id = ap.Id }));
                var housePlayers = UnitOfWork.PlayerRepository.Get(true).AsQueryable()
                                .Where(hp => awayPlayerMatchs.Any(pm => pm.PlayerId == hp.Id && hp.RedCard == 1 && hp.YellowCard == 2))
                                .ForEach(hp => improperPlayers.Add(new ImproperAligmentPlayerDto { Id = hp.Id }));

            }

            return improperPlayers;
        }
    }
}
