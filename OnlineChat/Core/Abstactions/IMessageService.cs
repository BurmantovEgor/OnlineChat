using CSharpFunctionalExtensions;
using OnlineChat.Applicaton.DTO_s;
using OnlineChat.Core.DTO_s;
using OnlineChat.Data.Models;

namespace OnlineChat.Core.Abstactions
{
    public interface IMessageService
    {
        Task<Result<MessageAddDTO>> SaveMessage(MessageAddDTO message);
        Task<Result<List<MessageGetDTO>>> GetLastMessages(DateTime start, DateTime end);

    }
}
