using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PresentIT.Models;

namespace PresentIT
{
    public class PITContext : DbContext
    {
        public PITContext(DbContextOptions<PITContext> options)
            : base(options)
        {
        }

        public DbSet<PresentIT.Models.Company> Company { get; set; }

        public DbSet<PresentIT.Models.Candidate> Candidate { get; set; }
    }
}