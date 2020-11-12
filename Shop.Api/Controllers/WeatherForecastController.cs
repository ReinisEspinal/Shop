using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Shop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var lst = new List<WeatherForecast>() 
            {
                new WeatherForecast(){  Id=1, Nombre="Francis Ramirez"},
                new WeatherForecast(){  Id=2, Nombre="Jose Perez"},
                new WeatherForecast(){  Id=3, Nombre="Juan Garcia"},
            };

            return lst;
             
        }
    }
}
