using AutoMapper;
using BusinessLogicLayer.Mappings.Resolvers;
using Models.DataTransferObjects;
using Models.DataTransferObjects.Creating;
using Models.Entities;

namespace BusinessLogicLayer.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserRegistrationDto, User>().ForMember(user => user.Login, dto => dto.MapFrom(from => from.Login))
                                                  .ForMember(user => user.PasswordHash, dto => dto.ResolveUsing<PasswordToHashResolver>());

            CreateMap<User, UserDto>().ForMember(dto => dto.Id, user => user.MapFrom(from => from.Id))
                                      .ForMember(dto => dto.Login, dto => dto.MapFrom(from => from.Login))
                                      .ForMember(dto => dto.AvatarUrl, user => user.MapFrom(from => from.AvatarUrl));

            CreateMap<GameCreatingDto, Game>().ForMember(game => game.Title, dto => dto.MapFrom(from => from.Title))
                                              .ForMember(game => game.About, dto => dto.MapFrom(from => from.About))
                                              .ForMember(game => game.ImageSrc, dto => dto.MapFrom(from => from.ImageSrc));

            CreateMap<Game, GameDto>().ForMember(dto => dto.Title, game => game.MapFrom(from => from.Title))
                                      .ForMember(dto => dto.About, game => game.MapFrom(from => from.About))
                                      .ForMember(dto => dto.ImageSrc, game => game.MapFrom(from => from.ImageSrc));

            CreateMap<ResultCreatingDto, Result>().ForMember(result => result.UserId, dto => dto.MapFrom(from => from.UserId))
                                                  .ForMember(result => result.GameId, dto => dto.MapFrom(from => from.GameId))
                                                  .ForMember(result => result.Score, dto => dto.MapFrom(from => from.Score));

            CreateMap<Result, ResultDto>().ForMember(dto => dto.Id, result => result.MapFrom(from => from.Id))
                                          .ForMember(dto => dto.Score, result => result.MapFrom(from => from.Score))
                                          .ForMember(dto => dto.UserId, result => result.MapFrom(from => from.UserId))
                                          .ForMember(dto => dto.UserLogin, result => result.MapFrom(from => from.User.Login))
                                          .ForMember(dto => dto.UserAvatarUrl, result => result.MapFrom(from => from.User.AvatarUrl))
                                          .ForMember(dto => dto.GameId, result => result.MapFrom(from => from.GameId))
                                          .ForMember(dto => dto.GameTitle, result => result.MapFrom(from => from.Game.Title));
        }
    }
}