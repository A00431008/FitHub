using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitHub.Models;

namespace FitHub.Data
{
    public class SwimmingPoolContext : DbContext
    {
        public SwimmingPoolContext (DbContextOptions<SwimmingPoolContext> options)
            : base(options)
        {
        }

        public DbSet<FitHub.Models.SwimmingPool> SwimmingPool { get; set; } = default!;
    }
}
