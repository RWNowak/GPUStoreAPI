using GPUStoreAPI.Data;
using GPUStoreAPI.Models;
using GPUStoreAPI.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Web.Http.ModelBinding;

namespace GPUStoreAPI.Controllers
{
    [Route("api/GPUStoreAPI")]
    [ApiController]
    public class GPUStoreAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        public GPUStoreAPIController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<GPUDTO>> GetGPUs() 
        {
            return Ok(_db.GPUs.ToList());
        }

        [HttpGet("{id:int}", Name="GetGPU")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<GPUDTO> GetGPU(int id)
        {
            var gpu = _db.GPUs.FirstOrDefault(u => u.ID == id);
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
                GPU model = new()
                {
                    ID = gpuDTO.ID,
                    Name = gpuDTO.Name,
                    Price = gpuDTO.Price,
                    MemoryType = gpuDTO.MemoryType,
                    Memory = gpuDTO.Memory,
                    Chip = gpuDTO.Chip
                };

                _db.GPUs.Add(model);
                _db.SaveChanges();
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
        public IActionResult DeleteGPU(int id) 
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var gpu = _db.GPUs.FirstOrDefault(u => u.ID == id);
            if (gpu == null)
            {
                return NotFound();
            }
            _db.GPUs.Remove(gpu);
            _db.SaveChanges();

            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateGPU")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateGPU(int id, [FromBody] GPUDTO gpuDTO)
        {
            if (gpuDTO == null || id != gpuDTO.ID)
            {
                return BadRequest();
            }
            GPU model = new()
            {
                ID = gpuDTO.ID,
                Name = gpuDTO.Name,
                Price = gpuDTO.Price,
                MemoryType = gpuDTO.MemoryType,
                Memory = gpuDTO.Memory,
                Chip = gpuDTO.Chip
            };
            _db.GPUs.Update(model);
            _db.SaveChanges();
            return NoContent();
        }
        //[HttpPatch("{id:int}", Name = "UpdatePartialGPU")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult UpdatePartialGPU(int id, JsonPatchDocument<GPUDTO> patchDTO)
        //{
        //    if (patchDTO == null || id == 0)
        //    {
        //        return BadRequest();
        //    }
        //    var gpu = _db.GPUs.FirstOrDefault(u => u.ID == id);

        //    GPUDTO gpudto = new()
        //    {
        //        ID = gpu.ID,
        //        Name = gpu.Name,
        //        Price = gpu.Price,
        //        MemoryType = gpu.MemoryType,
        //        Memory = gpu.Memory,
        //        Chip = gpu.Chip
        //    };

        //    if (gpu == null)
        //    {
        //        return BadRequest();
        //    }
        //    patchDTO.ApplyTo(gpudto, ModelState);
        //    GPU model = new()
        //    {
        //        ID = gpu.ID,
        //        Name = gpu.Name,
        //        Price = gpu.Price,
        //        MemoryType = gpu.MemoryType,
        //        Memory = gpu.Memory,
        //        Chip = gpu.Chip
        //    };
        //    _db.GPUs.Update(model);
        //    _db.SaveChanges();
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    return NoContent();
        //}

    }
}
