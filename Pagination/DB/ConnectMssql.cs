using Microsoft.EntityFrameworkCore;
using Pagination.Models;

namespace Pagination.DB
{
    public class ConnectMssql : DbContext
    {
        public ConnectMssql(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> users { get; set; }
    }
}
