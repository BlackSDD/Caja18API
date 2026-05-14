using Caja18API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Caja18API.Controllers
{
    [ApiController]
    [Route("api/dispositivos")]
    public class DispositivosController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public DispositivosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        [HttpGet]
        public async Task<IActionResult> Get() 
        {

            try
            {
                var response = await _httpClient.GetAsync("https://api.restful-api.dev/objects");

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode(500);
                }
                var Respuesta = await response.Content.ReadAsStringAsync();

                var Salida = JsonSerializer.Deserialize<List<Dispositivos>>
                    (
                    Respuesta, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                    );
                return Ok(Salida);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {

                var response = await _httpClient.GetAsync($"https://api.restful-api.dev/objects/{id}");

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound("Dispositivo no encontrado");
                }

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode(500);
                }

                var Respuesta = await response.Content.ReadAsStringAsync();

                var Salida = JsonSerializer.Deserialize<Dispositivos>
                    (
                    Respuesta, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                    );
                return Ok(Salida);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DispositivoPost Envio  )
        {
            try
            {

                var JsonEnvio = JsonSerializer.Serialize(Envio);

                


                var EnvioCompleto = new StringContent(
                JsonEnvio,
                Encoding.UTF8,
                "application/json");


                var response = await _httpClient.PostAsync($"https://api.restful-api.dev/objects", EnvioCompleto);


                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode(500);
                }

                var respuesta = await response
                    .Content
                    .ReadAsStringAsync();

                var salida = new
                {
                    message = "Dispositivo creado correctamente"
                };

                return Ok(salida);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
