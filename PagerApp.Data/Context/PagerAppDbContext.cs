using Microsoft.EntityFrameworkCore;
using PagerApp.Domain.Entities;

namespace PagerApp.Data.Context
{
    public class PagerAppDbContext : DbContext
    {
        public PagerAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Note> Notes { get; set; }
    }
}
