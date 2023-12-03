using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitHub.Models;

namespace FitHub.Data
{
    public class SaunaContext : DbContext
    {
        public SaunaContext (DbContextOptions<SaunaContext> options)
            : base(options)
        {
        }

        public DbSet<FitHub.Models.Sauna> Sauna { get; set; } = default!;
    }
}
