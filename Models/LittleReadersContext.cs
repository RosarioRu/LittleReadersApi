using Microsoft.EntityFrameworkCore;

namespace LittleReaders.Models
{
    public class LittleReadersContext : DbContext
    {
        public LittleReadersContext(DbContextOptions<LittleReadersContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}