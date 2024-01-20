using MediatR;
using ProjectName.ServiceName.Application.Common.Interfaces;
using ProjectName.ServiceName.Application.Common.Mappings;
using ProjectName.ServiceName.Application.Common.Models;

namespace ProjectName.ServiceName.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public record GetTodoItemsWithPaginationQuery : IRequest<PaginatedList<TodoItemBriefDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class
    GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTodoItemsWithPaginationQuery,
        PaginatedList<TodoItemBriefDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<TodoItemBriefDto>> Handle(GetTodoItemsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
       return await PaginatedList<TodoItemBriefDto>.CreateAsync(_context.TodoItems
            .Where(x => x.ListId == request.ListId)
            .OrderBy(x => x.Title)
            .ProjectToDto(), request.PageNumber, request.PageSize);
    }
}
