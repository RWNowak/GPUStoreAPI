using GPUStoreAPI.Data;
using GPUStoreAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

        [HttpGet("{id:int}", Name="GetGPU")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<GPUDTO> AddGPU([FromBody] GPUDTO gpuDTO)
        {
            if (gpuDTO == null)
            {
                return BadRequest(gpuDTO);
            }
            if (gpuDTO.ID > 0)
            {
                return Conflict("A GPU with the specified ID already exists.");
            }
            if (gpuDTO.Price <= 0)
            {
                return BadRequest("Price must be greater than zero.");
            }
            if (string.IsNullOrWhiteSpace(gpuDTO.Chip) ||
                string.IsNullOrWhiteSpace(gpuDTO.MemoryType) ||
                string.IsNullOrWhiteSpace(gpuDTO.Memory))
            {
                return BadRequest("Chip, MemoryType, and Memory are required fields.");
            }
            try
            {
                GPUStore.GPUList.Add(gpuDTO);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return CreatedAtRoute("GetGPU", new { id = gpuDTO.ID }, gpuDTO);
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteGPU")]
        public IActionResult DeleteGPU(int id) //IActionResult does not allow to specify return types
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var gpu = GPUStore.GPUList.FirstOrDefault(u => u.ID == id);
            if (gpu == null)
            {
                return NotFound();
            }
            GPUStore.GPUList.Remove(gpu);

            return NoContent();
        }
    }
}
