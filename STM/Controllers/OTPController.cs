using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace STM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OTPController : ControllerBase
    {
        private readonly ILogger<OTPController> _logger;

        public OTPController(ILogger<OTPController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetOTP")]
        public async Task<OTP> Get()
        {
            using var client = new HttpClient();
            var result = await client.GetStringAsync("https://catfact.ninja/fact");
            Console.WriteLine(result);
            dynamic json = JsonConvert.DeserializeObject(result);
            return new OTP
            {
                RandomCatFact = json.fact
            };
        }
    }
}