using System;
using System.Collections.Generic;
using System.Linq;

namespace Football.Domain.MainBoundleContext
{
    public class Match : Entity
    {
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
