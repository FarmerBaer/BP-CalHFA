using Microsoft.EntityFrameworkCore;

namespace BP_CalHFA.Data
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            
        }
        // CalHFADB = joined table name in SQL
        public DbSet<CalHFA> CalHFADB  { get; set; }
    }
}
