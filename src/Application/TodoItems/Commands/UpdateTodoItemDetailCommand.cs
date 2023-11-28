using MediatR;
using ProjectName.ServiceName.Application.Common.Interfaces;
using ProjectName.ServiceName.Domain.Enums;

namespace ProjectName.ServiceName.Application.TodoItems.Commands.UpdateTodoItemDetail;

public record UpdateTodoItemDetailCommand : IRequest
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }
}

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        //TODO: do something

        await _context.SaveChangesAsync(cancellationToken);
    }
}
