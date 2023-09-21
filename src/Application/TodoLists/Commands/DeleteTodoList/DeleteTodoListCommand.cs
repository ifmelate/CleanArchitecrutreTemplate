using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectName.ServiceName.Application.Common.Exceptions;
using ProjectName.ServiceName.Application.Common.Interfaces;

namespace ProjectName.ServiceName.Application.TodoLists.Commands.DeleteTodoList;

public record DeleteTodoListCommand(int Id) : IRequest;

public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
    {
        // TODO: do something

        await _context.SaveChangesAsync(cancellationToken);
    }
}
