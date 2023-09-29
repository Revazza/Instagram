using Instagram.Application.Common;
using Instagram.Application.Interfaces;
using MediatR;

namespace Instagram.Application.Commands.Chats.DeleteChat;

public record DeleteChatCommand() : IRequest<Response>;

public class DeleteChatCommandHandler : IRequestHandler<DeleteChatCommand, Response>
{
    private readonly IChatRepository _chatRepository;

    public DeleteChatCommandHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Response> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
    {
        var chats = _chatRepository.GetAll();
        
        foreach (var chat in chats)
        {
            _chatRepository.Delete(chat);
        }

        await _chatRepository.SaveChangesAsync();
        return Response.Ok();
    }
}