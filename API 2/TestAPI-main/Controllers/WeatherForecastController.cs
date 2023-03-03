using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace TESTAPI.Controllers
{
    //Дата класс для хранения данных    
    public class WeatherData
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Degree { get; set; }
        public string Location { get; set; }

    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //Заполнение данных для тестирования
        public static List<WeatherData> weatherdatas = new() 
        { 
            new WeatherData() {Id = 1, Date = "21.01.2022", Degree = 10, Location = "Мурманск"},
            new WeatherData() {Id =23, Date = "10.08.2019", Degree = 20, Location = "Пермь"},
            new WeatherData() {Id = 24, Date = "05.11.2010", Degree = 15, Location = "Омск"},
            new WeatherData() {Id = 25, Date = "07.02.2021", Degree = 0, Location = "Томск"},
            new WeatherData() {Id = 26, Date = "30.05.2022", Degree = 3, Location = "Калининград"},

        };


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //Получение всех данных, присутствует необязательный параметр sortStrategy, который позволяет получить данные в отсортированном виде
        [HttpGet]
        public IActionResult GetAll(int? sortStrategy)
        {
            //Если передать 1 то данные будут отсортерованы от меньшего к большему
            if (sortStrategy == 1)
            {
                //Сортировка происходит по Id записи, при успешной сортировке возвращает положительный Request
                return Ok(weatherdatas.OrderBy(x => x.Id).ToList());
            }
            //При передачи -1 в качестве парамера данные будут отсортирован в обратном порядка
            else if (sortStrategy == -1)
            {
                return Ok(weatherdatas.OrderBy(x => x.Id).Reverse().ToList());
            } 
            //Если не передавать ничего то вернет данные в таком виде, как они лежат изначально 
            else if(sortStrategy == null)
            {
                return Ok(weatherdatas);
            }
            //При передаче в параметр какой то ерунды, вернет ошибку с пояснением
            else
            {
                return BadRequest("Некорректное значение параметра sortStrategy.");
            }
            
        }

        //Получение одной записи по Id (обязательный параметр)
        [HttpGet("{Id}")]
        public IActionResult GetOne(int Id)
        {
            //Проверка на то что такой Id существует в базе
            if (Id < 0 || Id >= weatherdatas.Count)
            {
                //Возвращает Request с ошибкой при неверном Id
                return BadRequest("Неверный индекс!");
            }
            //При помощи цикла происходит поиск нужной записи
            for (int i = 0; i < weatherdatas.Count; i++)
            {
                //Если запись нашлась, то возвращает положительный Request с записью
                if (weatherdatas[i].Id == Id)
                {
                    return Ok(weatherdatas[i]);
                }
            }
            return BadRequest("Такой записи не обнаружено!");
        }

        //Добавление новых записей
        [HttpPost]
        public IActionResult Add(WeatherData data)
        {
            if (data.Id < 0 || data.Id >= weatherdatas.Count)
            {
                return BadRequest("Неверный индекс!");
            }
            for (int i = 0; i < weatherdatas.Count; i++)
            {
                if (weatherdatas[i].Id == data.Id)
                {
                    return BadRequest("Такой Id уже есть.");
                }
            }
            weatherdatas.Add(data);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(WeatherData data)
        {
            if (data.Id < 0 || data.Id >= weatherdatas.Count)
            {
                return BadRequest("Неверный индекс!");
            }
            for (int i = 0; i < weatherdatas.Count; i++)
            {
                if (weatherdatas[i].Id == data.Id)
                {
                    weatherdatas[i] = data;
                    return Ok();
                }
            }
            return BadRequest("Такая запись не обнаружена.");
        }

        [HttpDelete]
        public IActionResult Delete(WeatherData data)
        {
            if (data.Id < 0 || data.Id >= weatherdatas.Count)
            {
                return BadRequest("Неверный индекс!");
            }
            for (int i = 0; i < weatherdatas.Count; i++)
            {
                if (weatherdatas[i].Id == data.Id)
                {
                    weatherdatas.RemoveAt(i);
                    return Ok();
                }
            }
            return BadRequest("Такая запись не обнаружена.");
        }



        [HttpGet("find-by-city")]
        public IActionResult GetByCityName(string location)
        {
            for (int i = 0; i < weatherdatas.Count; i++)
            {
                if (weatherdatas[i].Location.ToLower() == location.ToLower())
                {
                    return Ok("Запись с указанным городом имеется в нашем списке");
                }
            }
            return BadRequest("Запись с указанным городом не обнаружено");
        }
    }
}