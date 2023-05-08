using ExpenseMVC.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMVC.Data
{
    public class MvcDbContext : DbContext
    {
        public MvcDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
    }
}
