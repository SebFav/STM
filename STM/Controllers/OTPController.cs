using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<OTP> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new OTP
            {
                Time = index
            })
            .ToArray();
        }
    }
}