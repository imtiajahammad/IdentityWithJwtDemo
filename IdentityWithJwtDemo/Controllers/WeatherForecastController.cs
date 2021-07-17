using IdentityWithJwtDemo.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.Controllers
{
    //[Authorize]
    //[Authorize(Roles =UserRoles.Admin)]
    
    [ApiController]
    //[Route("[controller]")]
    [Route("api/[controller]")]
    //[Authorize(Roles = UserRoles.Admin)]
    //[Authorize(Policy = "DeleteRolePolicy")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var ex = HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
            //_logger.LogError($"The path {ex.Path} threw an exception"+ $"{ex.Error}");
            _logger. LogError("The path pathdirectory threw an exception errorerror" );
            _logger.LogTrace("trace log");
            _logger.LogDebug("Debug log");
            _logger.LogInformation("Information log");
            _logger.LogWarning("Warning log");
            _logger.LogError("error log");
            _logger.LogCritical("Critical log");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
