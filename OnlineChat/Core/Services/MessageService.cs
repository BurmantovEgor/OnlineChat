using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.SignalR;
using OnlineChat.Applicaton.DTO_s;
using OnlineChat.Core.Abstactions;
using OnlineChat.Core.DTO_s;
using OnlineChat.Data.Abstractions;
using OnlineChat.Data.Models;

namespace OnlineChat.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepo;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository repository, IMapper mapper)
        {
            _messageRepo = repository;
            _mapper = mapper;
        }

        public async Task<Result<MessageAddDTO>> SaveMessage(MessageAddDTO message)
        {
            if (message == null) return Result.Failure<MessageAddDTO>("Ошибка при получении сообщения");
            if (message.Content.Length > 128 || String.IsNullOrWhiteSpace(message.Content)) 
                return Result.Failure<MessageAddDTO>("Сообщение должно быть длиной от 1 до 128 символов");
            var messageEntity = _mapper.Map<MessageEntity>(message);
            var reuslt = await _messageRepo.InsertMessage(messageEntity);
            if (reuslt.IsFailure) 
                return Result.Failure<MessageAddDTO>(reuslt.Error);
            return Result.Success<MessageAddDTO>(message);
        }

        public async Task<Result<List<MessageGetDTO>>> GetLastMessages(DateTime start, DateTime end)
        {
            if (start > end) return Result.Failure<List<MessageGetDTO>>("Некорректно задан временной промежуток");
            var result = await _messageRepo.SelectMessage(start, end);
            if (result.IsFailure) return Result.Failure<List<MessageGetDTO>>(result.Error);
            var messageDTOList = _mapper.Map<List<MessageGetDTO>>(result.Value);
            return Result.Success(messageDTOList);
        }

       
    }
}
