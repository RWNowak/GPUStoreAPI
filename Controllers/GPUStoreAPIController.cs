using GPUStoreAPI.Data;
using GPUStoreAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GPUStoreAPI.Controllers
{
    [Route("api/GPUStoreAPI")]
    [ApiController]
    public class GPUStoreAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<GPUDTO>> GetGPUs() 
        {
            return Ok(GPUStore.GPUList);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<GPUDTO> GetGPU(int id)
        {
            var gpu = GPUStore.GPUList.FirstOrDefault(u => u.ID == id);
            if (id == 0)
            {
                return BadRequest();
            }
            if (gpu == null)
            {
                return NotFound();
            }
            return Ok(gpu);
        }
    }
}
