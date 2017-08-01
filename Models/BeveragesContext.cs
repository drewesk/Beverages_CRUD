using Microsoft.EntityFrameworkCore;

namespace BeverageAPI.Models
{
    public class BeverageContext : DbContext
    {
        public BeverageContext(DbContextOptions<BeverageContext> options)
            : base(options)
        {
        }

        public DbSet<BeverageItem> BeverageItems { get; set; }

    }
}