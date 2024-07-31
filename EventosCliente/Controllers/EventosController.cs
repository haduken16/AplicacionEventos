using EventosCliente.Models;
using EventosServer.Migrations;
using EventosServer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace EventosCliente.Controllers
{
    public class EventoController : Controller
    {
        private readonly HttpClient _httpClient;

        public EventoController(IHttpClientFactory httpClientFactory) {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5121/api");
        }
        public async Task<IActionResult> Index() {
            var response = await _httpClient.GetAsync("api/Eventos/Lista");

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var eventos = JsonConvert.DeserializeObject<IEnumerable<EventoViewModel>>(content);
                return View("Index", eventos);
            }

            return View(new List<EventoViewModel>());
        }

        public IActionResult Create() { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventoViewModel evento)
        {
            if (ModelState.IsValid) {
                var json = JsonConvert.SerializeObject(evento);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Eventos/Crear", content);
                if (response.IsSuccessStatusCode) {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el evento");
                }
            }

            return View(evento);
        }
    }
}
