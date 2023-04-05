using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;
using CommandsService.Dtos;

namespace CommandService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //Source --> Target
            CreateMap<Platform, PlatformReadDto>();
            // CreateMap<PlatformReadDto, Platform>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<PlatformPublishedDto, Platform>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
        }
    }
}