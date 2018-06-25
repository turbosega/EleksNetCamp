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
                                      .ForMember(user => user.PasswordHash,
                                                 dto => dto.ResolveUsing<PasswordHashResolver>());
        }
    }
}