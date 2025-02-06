using AutoMapper;
using OnlineChat.Applicaton.DTO_s;
using OnlineChat.Core.DTO_s;
using OnlineChat.Data.Models;

namespace OnlineChat.Core.Services
{
    public class MessageMapper : Profile
    {
        public MessageMapper()
        {
            CreateMap<MessageAddDTO, MessageEntity>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.date, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.message_num, opt => opt.MapFrom(src => src.MessageNubmer));

            CreateMap<MessageEntity, MessageGetDTO>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.content))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.date))
                .ForMember(dest => dest.MessageNubmer, opt => opt.MapFrom(src => src.message_num));

        }





    }
}
