using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace W3CService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForwardController: ControllerBase
    {
        private readonly HttpClient httpClient;
        public ForwardController(HttpClient httpclient)
        {
            this.httpClient = httpclient;
        }

        // POST api/values
        [HttpPost]
        public async Task<string> Post([FromBody]Data data)
        {
            if (data?.url == null)
            {
                return "all done";
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, data.url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(data.arguments), Encoding.UTF8,
                    "application/json")
            };
            var response = await httpClient.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
    }

    public class Data
    {
        public string url { get; set; }
        public Data arguments { get; set; }
    }
}
