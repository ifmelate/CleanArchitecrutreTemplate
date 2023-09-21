using ProjectName.ServiceName.Application.Common.Mappings;
using ProjectName.ServiceName.Domain.Entities;

namespace ProjectName.ServiceName.Application.Common.Models;

// Note: This is currently just used to demonstrate applying multiple IMapFrom attributes.
public class LookupDto : IMapFrom<TodoList>, IMapFrom<TodoItem>
{
    public int Id { get; init; }

    public string? Title { get; init; }
}
