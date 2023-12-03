using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitHub.Models;

namespace FitHub.Data
{
    public class SpaContext : DbContext
    {
        public SpaContext (DbContextOptions<SpaContext> options)
            : base(options)
        {
        }

        public DbSet<FitHub.Models.Spa> Spa { get; set; } = default!;
    }
}
