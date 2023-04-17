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
        public IEnumerable<GPUDTO> GetGPUs() 
        {
            return GPUStore.GPUList;
        }

        [HttpGet("{id:int}")]
        public GPUDTO GetGPU(int id)
        {
            return GPUStore.GPUList.FirstOrDefault(u => u.ID == id);
        }
    }
}
