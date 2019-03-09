using Microsoft.EntityFrameworkCore;

namespace Dashboard.API.Models
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Server> Servers { get; set; }
    }
}