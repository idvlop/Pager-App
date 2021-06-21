using Microsoft.EntityFrameworkCore;
using PagerApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerApp.Data.Context
{
    public class PagerAppDbContext : DbContext
    {
        public PagerAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Note> Notes { get; set; }

    }
}
