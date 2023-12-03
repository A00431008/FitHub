using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitHub.Models;

namespace FitHub.Data
{
    public class AmenityContext : DbContext
    {
        public AmenityContext (DbContextOptions<AmenityContext> options)
            : base(options)
        {
        }

        public DbSet<FitHub.Models.Amenity> Amenity { get; set; } = default!;
    }
}
