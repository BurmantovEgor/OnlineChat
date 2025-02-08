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
        private readonly IHubContext<MessageHub> _hubContext;

        public MessageService(IMessageRepository repository, IMapper mapper, IHubContext<MessageHub> hubContext)
        {
            _messageRepo = repository;
            _mapper = mapper;
            _hubContext = hubContext;
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

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", messageEntity);
            return Result.Success<MessageAddDTO>(message);
        }

        public async Task<Result<List<MessageGetDTO>>> GetLastMessages(DateTime start, DateTime end)
        {
            if (start > end) return Result.Failure<List<MessageGetDTO>>("Некорректно задан временной промежуток");
            if (start.Equals(DateTime.MinValue) && end.Equals(DateTime.MinValue)) return Result.Failure<List<MessageGetDTO>>("Необходимо заполнить хотя-бы одно значения");

            var result = await _messageRepo.SelectMessage(start, end);
            if (result.IsFailure) return Result.Failure<List<MessageGetDTO>>(result.Error);
            var messageDTOList = _mapper.Map<List<MessageGetDTO>>(result.Value);
            return Result.Success(messageDTOList);
        }

       
    }
}
