using FacilitySense.DataModels;
using FacilitySense.IRepositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FacilitySense.AdminApp.Controllers
{
    [Route("api/facilities")]
    [ApiController]
    public class FacilitiesController : ControllerBase
    {
        private IFacilityRepository _facilityRepository;

        public FacilitiesController(IFacilityRepository facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }

        // GET: api/<FacilitiesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facility>>> Get()
        {
            var facilities = await _facilityRepository.GetAllAsync();
            return Ok(facilities);
        }

        // GET api/<FacilitiesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Facility>> GetAsync(int id)
        {
            Facility? facility = await _facilityRepository.GetByIdAsync(id);
            if(facility == null)
            {
                return NotFound();
            }
            return facility;
        }

        // POST api/<FacilitiesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Facility value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            await _facilityRepository.InsertAsync(value);
            return Ok();
        }

        // PUT api/<FacilitiesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Facility value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            await _facilityRepository.UpdateAsync(value);
            return Ok();
        }

        // DELETE api/<FacilitiesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _facilityRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
