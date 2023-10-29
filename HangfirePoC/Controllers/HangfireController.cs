using HangfirePoC.Services;
using HangfirePoC.Services.Interfaces;
using HangfirePoC.Viewmodels;
using Microsoft.AspNetCore.Mvc;

namespace HangfirePoC.Controllers
{
    /// <summary>
    /// Hangfire Proof of Concept.
    /// </summary>
    [ApiController]
    [Route("api/hangfire")]
    public class HangfireController : ControllerBase
    {
        private readonly IHangfireService _hangfireService;

        /// <summary>
        /// Constructor.
        /// </summary>
        public HangfireController(IHangfireService hangfireService)
        {
            _hangfireService = hangfireService;
        }

        /// <summary>
        /// Regular (a.k.a. enqueued) Hangfire job test.
        /// </summary>
        /// <param name="requestModel"><see cref="RegisterUserRequestModel"/></param>
        /// <returns>Welcome message.</returns>
        [HttpPost]
        [Route("registration/user")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RegisterUser([FromBody] RegisterUserRequestModel requestModel)
        {
            string response = _hangfireService.RegisterUser(requestModel);
            return Ok(response);
        }

        /// <summary>
        /// Delayed (a.k.a. scheduled) Hangfire job test.
        /// </summary>
        /// <param name="requestModel"><see cref="RegisterUserRequestModel"/></param>
        /// <returns>Welcome message.</returns>
        [HttpPost]
        [Route("registration/user/delayed")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RegisterUserDelayed([FromBody] RegisterUserDelayedRequestModel requestModel)
        {
            string response = _hangfireService.RegisterUserDelayed(requestModel);
            return Ok(response);
        }

        /// <summary>
        /// Register the user, simulating a time-expensive job test.
        /// </summary>
        /// <param name="requestModel"><see cref="RegisterUserRequestModel"/></param>
        /// <param name="computationTimeInMs">Simulated processing time in ms.</param>
        /// <returns>Welcome message.</returns>
        [HttpPost]
        [Route("registration/user/long-job")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RegisterUserExpensive([FromBody] RegisterUserRequestModel requestModel, [FromQuery] int computationTimeInMs = 7000)
        {
            string response = _hangfireService.RegisterUserExpensive(requestModel, computationTimeInMs);
            return Ok(response);
        }

        /// <summary>
        /// Register the user, simulating a time-expensive job and after that, starting another one test.
        /// </summary>
        /// <param name="requestModel"><see cref="RegisterUserRequestModel"/></param>
        /// <param name="computationTimeInMs">Simulated processing time in ms.</param>
        /// <returns>Welcome message.</returns>
        [HttpPost]
        [Route("registration/user/chain-job")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RegisterUserChain([FromBody] RegisterUserRequestModel requestModel, [FromQuery] int computationTimeInMs = 7000)
        {
	        string response = _hangfireService.RegisterUserChain(requestModel, computationTimeInMs);
	        return Ok(response);
        }
	}
}