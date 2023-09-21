using MediatR;
using ProjectName.ServiceName.Application.Common.Interfaces;

namespace ProjectName.ServiceName.Application.TodoItems.Commands.DeleteTodoItem;

public record DeleteTodoItemCommand(int Id) : IRequest;

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        //TODO: do something

        await _context.SaveChangesAsync(cancellationToken);
    }
}
