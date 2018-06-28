using AutoMapper;
using WebApi.Helpers.Mapping.Resolvers;
using WebApi.Models.DataTransferObjects;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto, User>().ForMember(user => user.Login, dto => dto.MapFrom(from => from.Login))
                                      .ForMember(user => user.PasswordHash, dto => dto.ResolveUsing<PasswordHashResolver>());

            CreateMap<GameDto, Game>().ForMember(game => game.Title, dto => dto.MapFrom(from => from.Title));

            CreateMap<ScoreDto, Score>().ForMember(score => score.UserId, dto => dto.MapFrom(from => from.UserId))
                                        .ForMember(score => score.GameId, dto => dto.MapFrom(from => from.GameId))
                                        .ForMember(score => score.Result, dto => dto.MapFrom(from => from.Result));
        }
    }
}