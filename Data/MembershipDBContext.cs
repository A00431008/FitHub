using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitHub.Models;

namespace FitHub.Data
{
    public class MembershipDBContext : DbContext
    {
        public MembershipDBContext (DbContextOptions<MembershipDBContext> options)
            : base(options)
        {
        }

        public DbSet<FitHub.Models.Membership> Membership { get; set; } = default!;
    }
}
