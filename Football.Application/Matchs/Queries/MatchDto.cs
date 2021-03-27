using AutoMapper;
using Football.Application.Commons.Mapping;
using Football.Domain.MainBoundleContext;
using System;
using System.Collections.Generic;

namespace Football.Application.Matchs.Queries
{
    public class MatchDto : IMapFrom<Match>
    {
        public int Id { get; set; }
        public List<PlayerDto> HouseTeamPlayers { get; set; }
        public List<PlayerDto> AwayTeamPlayers { get; set; }
        public ManagerDto HouseTeamManager { get; set; }
        public ManagerDto AwayTeamManager { get; set; }
        public RefereeDto Referee { get; set; }
        public DateTime Date { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Match, MatchDto>()
                .ForMember(m => m.HouseTeamManager, opt => opt.Ignore())
                .ForMember(m => m.AwayTeamManager, opt => opt.Ignore())
                .ForMember(m => m.HouseTeamPlayers, opt => opt.Ignore())
                .ForMember(m => m.AwayTeamPlayers, opt => opt.Ignore())
                ;
        }
    }
}
