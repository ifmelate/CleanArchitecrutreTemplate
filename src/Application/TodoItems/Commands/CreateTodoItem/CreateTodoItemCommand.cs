using FluentValidation;
using MediatR;
using ProjectName.ServiceName.Application.Common.Interfaces;

namespace ProjectName.ServiceName.Application.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand : IRequest<int>
{
    public int ListId { get; init; }

    public string? Title { get; init; }
}

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        //TODO: do something

        await _context.SaveChangesAsync(cancellationToken);

        return 0;
    }
}


[SuppressMessage("ReSharper", "UnusedType.Global")]
public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}
