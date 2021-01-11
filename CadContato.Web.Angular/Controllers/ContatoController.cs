using CadContato.Web.Angular.Models;
using CadContato.Web.Angular.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CadContato.Web.Angular.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private string url = "https://localhost:5001/api/v1/contato";
        private const string appJson = "Application/Json";

        IConfiguration configuration;

        public ContatoController(IConfiguration configuration)
        {
            this.configuration = configuration;

            url = configuration.GetValue<string>("UrlApi") + "contato";
        }

        [HttpGet]
        public async Task<object> GetAllAsync()
        {
            var call = HttpClientHelper.Client.GetAsync(url);
            var r = await call.Result.Content.ReadAsStringAsync();

            return r;
        }

        [HttpPost]
        public async Task<object> Insert([FromBody] Contato contato)
        {
            var cnt = new StringContent(JsonSerializer.Serialize(contato));
            cnt.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(appJson);

            var call = HttpClientHelper.Client.PostAsync(url, cnt);
            var r = await call.Result.Content.ReadAsStringAsync();

            return r;
        }

        [HttpPut]
        public async Task<object> Update([FromBody] Contato contato)
        {
            var cnt = new StringContent(JsonSerializer.Serialize(contato, new JsonSerializerOptions
            {
                IgnoreNullValues = true
            }));
            cnt.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(appJson);

            var call = HttpClientHelper.Client.PutAsync(url, cnt);
            var r = await call.Result.Content.ReadAsStringAsync();

            return r;
        }

        [HttpDelete]
        public async Task<object> Delete([FromQuery] string id)
        {
            var dto = new Contato();
            dto.Id = id;
            var req = new HttpRequestMessage(new HttpMethod("DELETE"), this.url);
            req.Content = new StringContent(JsonSerializer.Serialize(dto));
            req.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(appJson);

            var call = HttpClientHelper.Client.SendAsync(req);
            var r = await call.Result.Content.ReadAsStringAsync();

            return r;
        }


    }
}
