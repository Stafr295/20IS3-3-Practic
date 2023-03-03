using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace TESTAPI.Controllers
{
    //���� ����� ��� �������� ������    
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
        //���������� ������ ��� ������������
        public static List<WeatherData> weatherdatas = new() 
        { 
            new WeatherData() {Id = 1, Date = "21.01.2022", Degree = 10, Location = "��������"},
            new WeatherData() {Id =23, Date = "10.08.2019", Degree = 20, Location = "�����"},
            new WeatherData() {Id = 24, Date = "05.11.2010", Degree = 15, Location = "����"},
            new WeatherData() {Id = 25, Date = "07.02.2021", Degree = 0, Location = "�����"},
            new WeatherData() {Id = 26, Date = "30.05.2022", Degree = 3, Location = "�����������"},

        };


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //��������� ���� ������, ������������ �������������� �������� sortStrategy, ������� ��������� �������� ������ � ��������������� ����
        [HttpGet]
        public IActionResult GetAll(int? sortStrategy)
        {
            //���� �������� 1 �� ������ ����� ������������� �� �������� � ��������
            if (sortStrategy == 1)
            {
                //���������� ���������� �� Id ������, ��� �������� ���������� ���������� ������������� Request
                return Ok(weatherdatas.OrderBy(x => x.Id).ToList());
            }
            //��� �������� -1 � �������� �������� ������ ����� ������������ � �������� �������
            else if (sortStrategy == -1)
            {
                return Ok(weatherdatas.OrderBy(x => x.Id).Reverse().ToList());
            } 
            //���� �� ���������� ������ �� ������ ������ � ����� ����, ��� ��� ����� ���������� 
            else if(sortStrategy == null)
            {
                return Ok(weatherdatas);
            }
            //��� �������� � �������� ����� �� ������, ������ ������ � ����������
            else
            {
                return BadRequest("������������ �������� ��������� sortStrategy.");
            }
            
        }

        //��������� ����� ������ �� Id (������������ ��������)
        [HttpGet("{Id}")]
        public IActionResult GetOne(int Id)
        {
            //�������� �� �� ��� ����� Id ���������� � ����
            if (Id < 0 || Id >= weatherdatas.Count)
            {
                //���������� Request � ������� ��� �������� Id
                return BadRequest("�������� ������!");
            }
            //��� ������ ����� ���������� ����� ������ ������
            for (int i = 0; i < weatherdatas.Count; i++)
            {
                //���� ������ �������, �� ���������� ������������� Request � �������
                if (weatherdatas[i].Id == Id)
                {
                    return Ok(weatherdatas[i]);
                }
            }
            return BadRequest("����� ������ �� ����������!");
        }

        //���������� ����� �������
        [HttpPost]
        public IActionResult Add(WeatherData data)
        {
            if (data.Id < 0 || data.Id >= weatherdatas.Count)
            {
                return BadRequest("�������� ������!");
            }
            for (int i = 0; i < weatherdatas.Count; i++)
            {
                if (weatherdatas[i].Id == data.Id)
                {
                    return BadRequest("����� Id ��� ����.");
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
                return BadRequest("�������� ������!");
            }
            for (int i = 0; i < weatherdatas.Count; i++)
            {
                if (weatherdatas[i].Id == data.Id)
                {
                    weatherdatas[i] = data;
                    return Ok();
                }
            }
            return BadRequest("����� ������ �� ����������.");
        }

        [HttpDelete]
        public IActionResult Delete(WeatherData data)
        {
            if (data.Id < 0 || data.Id >= weatherdatas.Count)
            {
                return BadRequest("�������� ������!");
            }
            for (int i = 0; i < weatherdatas.Count; i++)
            {
                if (weatherdatas[i].Id == data.Id)
                {
                    weatherdatas.RemoveAt(i);
                    return Ok();
                }
            }
            return BadRequest("����� ������ �� ����������.");
        }



        [HttpGet("find-by-city")]
        public IActionResult GetByCityName(string location)
        {
            for (int i = 0; i < weatherdatas.Count; i++)
            {
                if (weatherdatas[i].Location.ToLower() == location.ToLower())
                {
                    return Ok("������ � ��������� ������� ������� � ����� ������");
                }
            }
            return BadRequest("������ � ��������� ������� �� ����������");
        }
    }
}