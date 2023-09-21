using ProjectName.ServiceName.Domain.Common;

namespace ProjectName.ServiceName.Domain.Entities;

public class TodoList : BaseAuditableEntity
{
    public string? Title { get; set; }


    public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
}
