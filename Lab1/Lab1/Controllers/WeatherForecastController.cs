using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using System.Xml.Linq;

namespace Lab1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public List<string> Get()
        {
            return Summaries;
        }
        [HttpPost]
        public IActionResult Add(string name)
        {
            if (name.Length <1)
            {
                return BadRequest("Такой индекс неверный!!!!");
            }
            Summaries.Add(name);
            return Ok();
        }
        [HttpPut]
        public IActionResult update(string name, int index) 
        {

            if (name.Length < 1)
            {
                return BadRequest("Такой индекс неверный!!!!");
            }
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Такой индекс неверный!!!!");
            }
            Summaries[index] = name;
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int index) 
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Такой индекс неверный!!!!");
            }
            Summaries.RemoveAt(index);
            return Ok();
        }
        [HttpGet("{index}")]
        public string getone(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return "Такой индекс неверный!!!!";
            }
            return Summaries[index];
        }
    }
}