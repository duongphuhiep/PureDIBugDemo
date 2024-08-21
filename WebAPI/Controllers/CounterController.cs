using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

using WeatherForecast;

[Route("[controller]")]
public class CounterController(ICounterService _counterService) : ControllerBase
{
    [HttpGet(Name = "GetCounter")]
    public long Get()
    {
        _counterService.IncrementCount();
        return _counterService.Count;
    }
}