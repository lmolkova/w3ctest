using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Threading;
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

        private async Task<string> CallNextAsync(string url, Data arguments)
        {
            if (url != null)
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(arguments), Encoding.UTF8, "application/json")
                };
                var response = await httpClient.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "all done";
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<string> Post([FromBody]Data data)
        {
            if (data?.actions != null)
            {
                foreach (var action in data.actions)
                {
                    if (action.sleep != null)
                    {
                        Thread.Sleep(TimeSpan.FromMilliseconds(action.sleep.Value));
                    }

                    await CallNextAsync(action.url, action.arguments);
                }
            }

            return await CallNextAsync(data?.url, data?.arguments);
        }
    }

    public class Action
    {
        public int? sleep { get; set; }
        public string url { get; set; }
        public Data arguments { get; set; }
    }

    public class Data
    {
        public string url { get; set; }
        public Data arguments { get; set; }
        public Action[] actions { get; set; }
    }
}
