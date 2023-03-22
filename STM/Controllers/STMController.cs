using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace STM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class STMController : ControllerBase
    {
        private readonly ILogger<STMController> _logger;

        public STMController(ILogger<STMController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetSTM")]
        public async Task<STM> Get(String origin, String destination)
        {
            using var client = new HttpClient();

            var resultOrigin = await client.GetStringAsync($"http://portainer.10.194.33.169.nip.io:49772/Route/GetAddress/{origin}");
            Console.WriteLine(resultOrigin);
            dynamic jsonOrigin = JsonConvert.DeserializeObject(resultOrigin);
            var resultDest = await client.GetStringAsync($"http://portainer.10.194.33.169.nip.io:49772/Route/GetAddress/{destination}");
            Console.WriteLine(resultDest);
            dynamic jsonDest = JsonConvert.DeserializeObject(resultDest);
            var currentTime = DateTime.Now.ToString("h:mmtt 'GMT-4'");
            var currentDate = DateTime.Now.ToString("MM-dd-yyyy");
            var result = await client.GetStringAsync($"http://3.97.194.226:8080/otp/routers/default/plan?fromPlace={jsonOrigin.position.lat},{jsonOrigin.position.lon}&toPlace={jsonDest.position.lat},{jsonDest.position.lon}&time={currentTime}&date={currentDate}&mode=TRANSIT,WALK&showIntermediateStops=true&locale=en&walkReluctance=5&additionalParameters=walkReluctance");
            Console.WriteLine(result);
            Console.WriteLine(currentTime);
            Console.WriteLine(currentDate);
            dynamic json = JsonConvert.DeserializeObject(result);
            int totalWalk = 0;
            int totalBus = 0;
            int totalMetro = 0;
            int totalTrain = 0;
            foreach (var item in json.plan.itineraries[0].legs)
            {
                if (item.mode == "WALK") totalWalk += item.duration.ToObject<int>();
                if (item.mode == "BUS") totalBus += item.duration.ToObject<int>();
                if (item.mode == "SUBWAY") totalMetro += item.duration.ToObject<int>();
                if (item.mode == "TRAIN") totalTrain += item.duration.ToObject<int>();
            }
            return new STM
            {
                DurationTotal = json.plan.itineraries[0].duration,
                DurationWalk = totalWalk,
                DurationBus = totalBus,
                DurationMetro = totalMetro,
                DurationTrain = totalTrain
            };
        }
    }
}