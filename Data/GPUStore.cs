using GPUStoreAPI.Models.DTO;

namespace GPUStoreAPI.Data
{
    public static class GPUStore
    {
        public static List<GPUDTO> GPUList = new List<GPUDTO>
        {
             new GPUDTO {ID = 1, Name="RTX 4090", Price=1599, Chip="AD102", Memory="24GB", MemoryType="GDDR6X" },
             new GPUDTO {ID = 2, Name="RX 6900 XT", Price=799, Chip="Navi21", Memory="16GB", MemoryType="GDDR6" }
        };
    }
}
