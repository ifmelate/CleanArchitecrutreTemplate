using ProjectName.ServiceName.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using ProjectName.ServiceName.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProjectName.ServiceName.Application.Common.Mappings;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[Mapper]
public static partial class Mapping
{
    public static partial IQueryable<TodoItemBriefDto> ProjectToDto(this IQueryable<TodoItem> q);
}
