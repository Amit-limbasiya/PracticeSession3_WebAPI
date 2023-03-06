using Microsoft.EntityFrameworkCore;
using PracticeSession3.Models;

namespace PracticeSession3.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options): base(options) { }

        public DbSet<UserProfile> Users { get; set; }
    }
}
