using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using static NBPapi.AskSellCurrency;
using static NBPapi.CurrencyModel;

namespace NBPapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        HttpClient client = new HttpClient();

        [HttpGet]
        [Route("GetByData/{code}/{date}/")]
        public async Task<IActionResult> GetByDate(string code, DateTime date)
        {
            string path = $"http://api.nbp.pl/api/exchangerates/rates/A/"+code+date+"/?format=json";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return Ok(await response.Content.ReadFromJsonAsync<Root>());
            }
            return BadRequest("unable to get the response");
        }
        [HttpGet]
        [Route("GetMinAndMax/{code}/{n}")]
        public async Task<IActionResult> GetMinAndMax(string code, int n)
        {
            string path = $"http://api.nbp.pl/api/exchangerates/rates/A/"+code+"/last/"+n+"/?format=json";
            HttpResponseMessage response = await client.GetAsync(path);
            

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                JObject obj = JObject.Parse(responseBody);

                Root rates = obj.ToObject<Root>();

                var rateMin = rates.rates.Min(a => a.mid);

                var rateMax = rates.rates.Max(a => a.mid);

                string finalMessage = "min : " + rateMin + " max : " + rateMax;

                return Ok(finalMessage);
            }
            return BadRequest("unable to get the response");
        }
        [HttpGet]
        [Route("DiffrenceAskSell/{code}/{n}")]
        public async Task<IActionResult> DiffrenceAskSell(string code, int n)
        {
            string path = $"http://api.nbp.pl/api/exchangerates/rates/c/" + code + "/last/" + n + "/?format=json";
            HttpResponseMessage response = await client.GetAsync(path);


            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                JObject obj = JObject.Parse(responseBody);

                Root2 rates = obj.ToObject<Root2>();

                var ask = rates.rates.Select(a => a.ask);

                var bid = rates.rates.Select(a => a.bid);

                var diffs = new List<double>();

                for (int i = 0; i < n; i++)
                {
                    diffs.Add(ask.ElementAt(i) - bid.ElementAt(i));
                }
                return Ok(diffs);
            }
            return BadRequest("unable to get the response");
        }

    }
}
