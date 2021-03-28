using System;
using System.Collections.Generic;
using System.Linq;

namespace Football.Domain.MainBoundleContext
{
    public class Match : Entity
    {
        // TODO: I have change the relationships of entity Match with Player in order to allow many to many relationship. 
        // I have added a third entity call PlayerMatch using TPH approach in order to allow the separation between away players and house players
        public long HouseManagerId { get; set; }
        public Manager HouseManager { get; set; }
        public long AwayManagerId { get; set; }
        public Manager AwayManager { get; set; }

        public List<HousePlayerMatch> HousePlayersMatch
        {
            get => PlayersMatch.OfType<HousePlayerMatch>().ToList();
        }
        public List<AwayPlayerMatch> AwayPlayersMatch
        {
            get => PlayersMatch.OfType<AwayPlayerMatch>().ToList();
        }
        public List<PlayerMatch> PlayersMatch { get; set; }

        public Referee Referee { get; set; }
        public DateTime Date { get; set; }
    }
}
