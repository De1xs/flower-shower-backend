using FlowerShowerService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlowerShowerService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<TestEntity> TestEntities { get; set; }
    }
}
