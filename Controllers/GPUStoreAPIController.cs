using GPUStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GPUStoreAPI.Controllers
{
    [Route("api/GPUStoreAPI")]
    [ApiController]
    public class GPUStoreAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<GPU> GetGPUs() 
        {
            
            return new List<GPU>
            {
                new GPU {ID = 1, Name="RTX 4090", Price=1599, Chip="AD102", Memory="24GB", MemoryType="GDDR6X" }, 
                new GPU {ID = 2, Name="RX 6900 XT", Price=799, Chip="Navi21", Memory="16GB", MemoryType="GDDR6" }

            };
        }
    }
}
