using Microsoft.EntityFrameworkCore;
using Web_11050.Models.Domain;

namespace Web_11050.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get;set; }
    }
}
