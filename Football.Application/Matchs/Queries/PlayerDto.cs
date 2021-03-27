using Football.Application.Commons.Mapping;
using Football.Domain.MainBoundleContext;

namespace Football.Application.Matchs.Queries
{
    public class PlayerDto : IMapFrom<Player>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YellowCard { get; set; }
        public int RedCard { get; set; }
        public int MinutesPlayed { get; set; }
    }
}
