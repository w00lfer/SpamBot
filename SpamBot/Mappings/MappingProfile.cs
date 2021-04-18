using AutoMapper;
using SpamBotApi.Models;
using SpamBotApi.Models.Dtos;

namespace SpamBotApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Receiver, GetReceiverDto>();
            CreateMap<CreateReceiverDto, Receiver>();
            CreateMap<UpdateReceiverDto, Receiver>();
        }
    }
}
