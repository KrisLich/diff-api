using DiffAPI.Models;
using DiffAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace DiffAPI.Controllers
{
    /// <summary>
    /// Controller class.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v1/diff/{id}")]
    public class DiffController : ControllerBase
    {
        private readonly IDiffService _diffService;
        private readonly ILogger<DiffController> _logger;
        //private readonly IMemoryCache _cache;


        /// <summary>
        /// Initializes a new instance of the <see cref="DiffController"/> class.
        /// </summary>
        /// <param name="diffService">The diff service.</param>
        /// <param name="logger">The logger.</param>
        public DiffController(IDiffService diffService, ILogger<DiffController> logger)
        {
            _diffService = diffService ?? throw new ArgumentNullException(nameof(diffService));
            _logger = logger;
        }

        /// <summary>
        /// Uploads left data to be diffed.
        /// </summary>
        /// <param name="id">The ID</param>
        /// <param name="data">The data</param>
        /// <returns>An <see cref="IActionResult"/></returns>
        [HttpPut("left")]
        [EnableRateLimiting("fixed")]
        public IActionResult UploadLeftData(string id, [FromBody] DataRequestModel data)
        {
            _logger.LogInformation($"Received PUT request for left data with ID [{id}] and value ({data.Data})");

            // Base64 decoding
            byte[] decodedData = Convert.FromBase64String(data.Data);

            _diffService.UploadLeftData(id, decodedData);
            return CreatedAtAction(nameof(UploadLeftData), new { id }, null);
        }

        /// <summary>
        /// Uploads right data to be diffed.
        /// </summary>
        /// <param name="id">The ID</param>
        /// <param name="data">The data</param>
        /// <returns>An <see cref="IActionResult"/></returns>
        [HttpPut("right")]
        [EnableRateLimiting("fixed")]
        public IActionResult UploadRightData(string id, [FromBody] DataRequestModel data)
        {
            _logger.LogInformation($"Received PUT request for right data with ID [{id}] and value ({data.Data})");

            // Base64 decoding
            byte[] decodedData = Convert.FromBase64String(data.Data);

            _diffService.UploadRightData(id, decodedData);
            return CreatedAtAction(nameof(UploadRightData), new { id }, null);
        }

        /// <summary>
        /// Gets the diff results.
        /// </summary>
        /// <param name="id">The ID</param>
        /// <returns>An <see cref="IActionResult"/></returns>
        [HttpGet]
        [EnableRateLimiting("fixed")]
        public IActionResult GetDiffResult(string id)
        {
            _logger.LogInformation($"Retrieving diff results for data with ID {id}");

            // Use GetOrCreate method to get the value from the cache or create a new one if it doesn't exist (Alternate way (not implemented))
            //var diffResult = _cache.GetOrCreate(id, entry =>
            //{
            //    entry.SlidingExpiration = TimeSpan.FromMinutes(5); // Cache expires after 5 minutes
            //    return _diffService.GetDiffResult(id);
            //});
            var diffResult = _diffService.GetDiffResult(id);
            if (diffResult != null)
            {
                _logger.LogInformation($"Sending found diff results for data with ID {id}");
                return Ok(diffResult);
            }
            else
            {
                _logger.LogWarning($"No data found for ID {id}");
                return NotFound();
            }
        }
    }
}

