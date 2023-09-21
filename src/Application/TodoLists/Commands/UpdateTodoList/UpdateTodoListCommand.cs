using MediatR;
using ProjectName.ServiceName.Application.Common.Exceptions;
using ProjectName.ServiceName.Application.Common.Interfaces;

namespace ProjectName.ServiceName.Application.TodoLists.Commands.UpdateTodoList;

public record UpdateTodoListCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }
}

public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        // TODO: do something

        await _context.SaveChangesAsync(cancellationToken);
    }
}
