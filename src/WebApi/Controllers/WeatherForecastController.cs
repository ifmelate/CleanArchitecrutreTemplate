using Microsoft.AspNetCore.Mvc;
using ProjectName.ServiceName.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace ProjectName.ServiceName.WebApi.Controllers;

public class WeatherForecastController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        return await Mediator.Send(new GetWeatherForecastsQuery());
    }
    
    [HttpPost]
    public Task<bool> TestPost()
    {
        return (Task<bool>) Task.CompletedTask;
    }
}
