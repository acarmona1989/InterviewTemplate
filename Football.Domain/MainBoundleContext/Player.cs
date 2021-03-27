using System.Collections.Generic;

namespace Football.Domain.MainBoundleContext
{
    public class Player : Entity
    {
        public string Name { get; set; }
        public int YellowCard { get; set; }
        public int RedCard { get; set; }
        public int MinutesPlayed { get; set; }
        public List<PlayerMatch> PlayerMatchs { get; set; }
    }
}
