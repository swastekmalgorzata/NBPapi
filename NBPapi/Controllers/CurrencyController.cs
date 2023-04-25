using Microsoft.AspNetCore.Mvc;
using static NBPapi.AskSellCurrency;
using static NBPapi.CurrencyModel;

namespace NBPapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();

        [HttpGet]
        [Route("GetByDate/{code}/{date}")]
        public async Task<IActionResult> GetByDate(string code, string date)
        {
            string path = $"http://api.nbp.pl/api/exchangerates/rates/A/"+code+"/"+date+"/?format=json";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                Root responseBody = await response.Content.ReadFromJsonAsync<Root>();
                var mid = responseBody.rates.Select(a => a.mid);
                return Ok("average for " +code+ " at " +date + " : "+mid.ElementAt(0));
            }
            return BadRequest("unable to get the response, check if your inputs are corerect");
        }
        [HttpGet]
        [Route("GetMinAndMax/{code}/{n}")]
        public async Task<IActionResult> GetMinAndMax(string code, int n)
        {
            string path = $"http://api.nbp.pl/api/exchangerates/rates/A/"+code+"/last/"+n+"/?format=json";

            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                Root responseBody = await response.Content.ReadFromJsonAsync<Root>();

                var rateMin = Math.Round(responseBody.rates.Min(a => a.mid),4);

                var rateMax = Math.Round(responseBody.rates.Max(a => a.mid), 4);

                string finalMessage = "min : " + rateMin + " max : " + rateMax;

                return Ok(finalMessage);
            }
            return BadRequest("unable to get the response, check if your inputs are corerect");
        }
        [HttpGet]
        [Route("DiffrenceAskSell/{code}/{n}")]
        public async Task<IActionResult> DiffrenceAskBid(string code, int n)
        {
            string path = $"http://api.nbp.pl/api/exchangerates/rates/c/" + code + "/last/" + n + "/?format=json";

            HttpResponseMessage response = await client.GetAsync(path);


            if (response.IsSuccessStatusCode)
            {
                Root2 responseBody = await response.Content.ReadFromJsonAsync<Root2>();

                var ask = responseBody.rates.Select(a => a.ask);

                var bid = responseBody.rates.Select(a => a.bid);

                var diffs = new List<double>();

                for (int i = 0; i < n; i++)
                {
                    double test = ask.ElementAt(i);
                    double test2 = bid.ElementAt(i);
                    double diff = Math.Round(test - test2, 4);
                    diffs.Add(diff);
                }
                return Ok(diffs);
            }
            return BadRequest("unable to get the response, check if your inputs are corerect");
        }

    }
}
