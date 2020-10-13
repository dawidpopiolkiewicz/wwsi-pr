using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PR.Patients.Model
{
    public class PrDataContext : DbContext
    {

        public PrDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Patients> Patients { get; set;}
    }
}
