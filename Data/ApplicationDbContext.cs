using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet8_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet8_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions) { }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}