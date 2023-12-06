using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitHub.Models;

namespace FitHub.Data
{
    public class MembDetailDBContext : DbContext
    {
        public MembDetailDBContext (DbContextOptions<MembDetailDBContext> options)
            : base(options)
        {
        }

        public DbSet<FitHub.Models.MembershipDetail> MembershipDetail { get; set; } = default!;
    }
}
