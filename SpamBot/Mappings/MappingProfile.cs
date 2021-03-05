using AutoMapper;
using SpamBotApi.Models;
using SpamBotApi.Models.Dtos;

namespace SpamBotApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Email, GetEmailDto>();
            CreateMap<CreateEmailDto, Email>();
            CreateMap<UpdateEmailDto, Email>();

            CreateMap<Receiver, GetReceiverDto>();
            CreateMap<CreateReceiverDto, Receiver>();
            CreateMap<UpdateReceiverDto, Receiver>();

            CreateMap<SentEmail, GetSentEmailDto>();
            CreateMap<CreateSentEmailDto, SentEmail>();
            CreateMap<UpdateSentEmailDto, SentEmail>();
        }
    }
}
