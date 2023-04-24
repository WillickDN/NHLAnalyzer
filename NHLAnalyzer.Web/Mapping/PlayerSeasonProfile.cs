using AutoMapper;
using NHLAnalyzer.Data.Entities;
using NHLAnalyzer.Web.ViewModels;

namespace NHLAnalyzer.Web.Mapping
{
    public class PlayerSeasonProfile : Profile
    {
        public PlayerSeasonProfile()
        {
            CreateMap<PlayerSeasonViewModel, PlayerSeason>();

            CreateMap<PlayerSeason, PlayerSeasonViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Player.PlayerName));
        }
    }
}
