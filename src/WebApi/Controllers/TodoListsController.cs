using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectName.ServiceName.Application.TodoLists.Commands.CreateTodoList;
using ProjectName.ServiceName.Application.TodoLists.Queries.GetTodos;

namespace ProjectName.ServiceName.WebApi.Controllers;

public class TodoListsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<TodosVm>> Get()
    {
        return await Mediator.Send(new GetTodosQuery());
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateTodoListCommand command)
    {
        return await Mediator.Send(command);
    }

   
}
