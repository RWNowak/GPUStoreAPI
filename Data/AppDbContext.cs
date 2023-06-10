using GPUStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GPUStoreAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        public DbSet<GPU> GPUs { get; set; }
    }
}
