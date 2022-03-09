namespace Backlog.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Historic> Historic { get; set; }
    }
}
