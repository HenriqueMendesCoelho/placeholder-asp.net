namespace PlaceHolder.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Historic> Historic { get; set; }
        public DbSet<UserAddress> Address { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Ticket>()
                .Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (Status.StatusEnum)Enum.Parse(typeof(Status.StatusEnum), v)
                );
        }
    }
}
