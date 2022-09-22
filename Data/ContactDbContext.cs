using ContactsApi.Model;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> contact { get; set; }

    }
}
