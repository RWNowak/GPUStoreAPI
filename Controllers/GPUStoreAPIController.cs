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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<GPUDTO>> GetGPUs() 
        {
            return Ok(GPUStore.GPUList);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
