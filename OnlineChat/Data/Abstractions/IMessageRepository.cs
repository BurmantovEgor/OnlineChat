using CSharpFunctionalExtensions;
using OnlineChat.Core.DTO_s;
using OnlineChat.Data.Models;

namespace OnlineChat.Data.Abstractions
{
    public interface IMessageRepository
    {
        Task<Result> InsertMessage(MessageEntity message);
        Task<Result<List<MessageEntity>>> SelectMessage(DateTime start, DateTime end);
    }
}
