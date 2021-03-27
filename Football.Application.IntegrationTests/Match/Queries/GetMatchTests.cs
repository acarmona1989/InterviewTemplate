using Football.Application.Matchs.Queries;
using Football.Domain.MainBoundleContext;
using Football.Domain.Persistence;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentAssertions;
using System.Linq;

namespace Football.Application.IntegrationTests.Match.Queries
{
    using static Testing;
    public class GetMatchTests
    {
        [Test]
        public async Task ShouldReturnAllMatchs()
        {
            //Arrange
            var matchMoqRepository = new Mock<IRepository<Domain.MainBoundleContext.Match>>();
            matchMoqRepository.Setup(m => m.Get(true))
                .Returns(GetMatchs());

            var managerMoqRepository = new Mock<IRepository<Manager>>();
            managerMoqRepository.Setup(m => m.GetByID(It.IsAny<int>()))
                                .Returns(new Manager
                                {
                                    Id = 1,
                                    Name = "Guardiola",
                                });
            var playerMatchMoqRepository = new Mock<IRepository<PlayerMatch>>();
            playerMatchMoqRepository.Setup(m => m.Get(It.IsAny<Expression<Func<PlayerMatch, bool>>>(), null, string.Empty, true))
                                    .Returns(GetPlayerMatch());

            Mock<IRepository<Player>> playerMoqRepository = new Mock<IRepository<Player>>();
            playerMoqRepository.Setup(m => m.Get(true))
                               .Returns(GetPlayers());

            var query = new GetMatchQuery();
            var result = await SendAsync(query);
            result.Data.Should().HaveCount(1);
            result.Data.First().AwayTeamPlayers.Should().HaveCount(2);
        }

        private IEnumerable<Player> GetPlayers()
        {
            return new List<Player>
            {
                new Player
                {
                    Id = 2,
                    Name = "Ronaldo",
                },
                new Player
                {
                    Id = 3,
                    Name = "Iker",
                },
                new Player
                {
                    Id = 4,
                    Name = "Gerard",
                },
                new Player
                {
                    Id = 6,
                    Name = "Jordi",
                },
            };
        }

        private IEnumerable<PlayerMatch> GetPlayerMatch()
        {
            return new List<PlayerMatch>
            {
                new AwayPlayerMatch
                {
                    MatchId = 1,
                    PlayerId = 1
                },
                new AwayPlayerMatch
                {
                    MatchId = 1,
                    PlayerId = 2
                },
                new HousePlayerMatch
                {
                    MatchId = 1,
                    PlayerId = 3
                },
                new HousePlayerMatch
                {
                    MatchId = 1,
                    PlayerId = 4
                },
            };
        }

        private IEnumerable<Domain.MainBoundleContext.Match> GetMatchs()
        {
            return new List<Domain.MainBoundleContext.Match>
            {
                new Domain.MainBoundleContext.Match
                {
                    Id = 1,
                    AwayManagerId = 2,
                    HouseManagerId = 1,
                }
            };
        }
    }
}
