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
        public ActionResult<GPUDTO> GetGPU(int id)
        {
            return Ok(GPUStore.GPUList.FirstOrDefault(u => u.ID == id));
        }
    }
}
