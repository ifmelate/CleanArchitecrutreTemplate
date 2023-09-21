using MediatR;
using ProjectName.ServiceName.Application.Common.Exceptions;
using ProjectName.ServiceName.Application.Common.Interfaces;

namespace ProjectName.ServiceName.Application.TodoItems.Commands.UpdateTodoItem;

public record UpdateTodoItemCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        //TODO: do something

        await _context.SaveChangesAsync(cancellationToken);
    }
}
