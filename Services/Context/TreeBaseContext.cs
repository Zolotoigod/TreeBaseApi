using Microsoft.EntityFrameworkCore;
using TreeBase.Models;

namespace TreeBase.Repositories.Context
{
    public class TreeBaseContext : DbContext
    {
        public TreeBaseContext(DbContextOptions<TreeBaseContext> options)
        : base(options)
        {
        }

        public DbSet<Node> Nodes { get; set; }
        public DbSet<LogRecord> LogRecords { get; set; }
    }
}
