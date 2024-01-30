using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using WebApp.MVC.Models;

namespace WebApp.MVC.Controllers
{
    public class WeatherController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7286/WeatherForecast");
        private readonly HttpClient _httpClient;
        public WeatherController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            List<WeatherForecast> forecasts = new List<WeatherForecast>();

            HttpResponseMessage response = _httpClient.GetAsync(baseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                forecasts = JsonConvert.DeserializeObject<List<WeatherForecast>>(data);
            }
            return View(forecasts);
        }
    }
}
