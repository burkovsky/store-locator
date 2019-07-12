using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoresRepository _repository;

        public StoresController(IStoresRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Looks for stores
        /// </summary>
        /// <remarks>
        /// GET /find-stores?lat=37.7857182&amp;lng=-122.4010508&amp;radius=50
        /// </remarks>
        /// <param name="lat">Must be from -90 to 90</param>
        /// <param name="lng">Must be from -180 to 180</param>
        /// <param name="radius">Must be more than 0</param>
        /// <response code="200">Returns list of stores</response>
        /// <response code="400">If some parameters are not passed validation</response>
        /// <response code="500">If something is critically broken</response>
        [Route("find-stores")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> Get(
            [Range(-90, 90)] double? lat = null,
            [Range(-180, 180)] double? lng = null,
            [Range(0, int.MaxValue)] int radius = 50)
        {
            if (!lat.HasValue || !lng.HasValue)
            {
                return Ok(await _repository.Get());
            }

            return Ok(await _repository.Get(lat.Value, lng.Value, radius));
        }
    }
}
