using Football.Application.Commons.Mapping;
using Football.Domain.MainBoundleContext;

namespace Football.Application.Matchs.Queries
{
    public class RefereeDto : IMapFrom<Referee>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MinutesPlayed { get; set; }
    }
}
