using FluentAssertions;
using Football.Application.Commons.Exceptions;
using Football.Application.Matchs.Commands;
using Football.Domain.MainBoundleContext;
using Football.Domain.Persistence;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;

namespace Football.Application.IntegrationTests.Match.Commands
{
    using static Testing;
    public class CreateMatchCommandTest
    {
        [Test]
        public void ShouldMatchCommandHaveNotEmptyValues()
        {
            //Arrange
            var command = new CreateMatchCommand
            {
                AwayTeam = new List<int>(),
                HouseTeam = new List<int>(),
            };

            //Assert
            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async System.Threading.Tasks.Task CreateMatchWithValidInputReturnSuccessTestAsync()
        {
            //Arrange
            Mock<IRepository<Domain.MainBoundleContext.Match>> matchMoqRepository = new Mock<IRepository<Domain.MainBoundleContext.Match>>();
            matchMoqRepository.Setup(m => m.InsertAtomic(It.IsAny<Domain.MainBoundleContext.Match>()))
                              .Verifiable();
            Mock<IRepository<Manager>> managerMoqRepository = new Mock<IRepository<Manager>>();
            managerMoqRepository.Setup(m => m.GetByID(It.IsAny<int>()))
                                .Returns(new Manager
                                {
                                    Id = 3,
                                    Name = "Test",
                                });
            Mock<IRepository<Referee>> refereeMoqRepository = new Mock<IRepository<Referee>>();
            refereeMoqRepository.Setup(m => m.GetByID(It.IsAny<int>()))
                              .Verifiable();
            Mock<IRepository<Player>> playerMoqRepository = new Mock<IRepository<Player>>();
            playerMoqRepository.Setup(m => m.Get(It.IsAny<bool>()))
                              .Verifiable();
            Mock<IRepository<PlayerMatch>> playerMatchMoqRepository = new Mock<IRepository<PlayerMatch>>();
            playerMatchMoqRepository.Setup(m => m.Insert(It.IsAny<PlayerMatch>()))
                              .Verifiable();

            var moqUnitOfWork = new Mock<IUnitOfWork>();
            moqUnitOfWork.Setup(uow => uow.MatchRepository).Returns(matchMoqRepository.Object);
            moqUnitOfWork.Setup(uow => uow.ManagerRepository).Returns(managerMoqRepository.Object);
            moqUnitOfWork.Setup(uow => uow.RefereeRepository).Returns(refereeMoqRepository.Object);
            moqUnitOfWork.Setup(uow => uow.PlayerRepository).Returns(playerMoqRepository.Object);
            moqUnitOfWork.Setup(uow => uow.PlayerMatchRepository).Returns(playerMatchMoqRepository.Object);

            var createMatchCommand = new CreateMatchCommand();

            var createMatchCommandHandler = new CreateMatchCommandHandler(moqUnitOfWork.Object);

            // Act
            await createMatchCommandHandler.Handle(createMatchCommand, CancellationToken.None);

            //Assert
            matchMoqRepository.Verify(m => m.InsertAtomic(It.IsAny<Domain.MainBoundleContext.Match>()), Times.Once,
                "CreateAsync must be called only once");

            moqUnitOfWork.Verify(m => m.Commit(), Times.Once, "Commit must be called only once");
        }
    }
}
