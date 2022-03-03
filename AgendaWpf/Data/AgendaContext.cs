using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaWpf.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaWpf.Data
{
    public partial class AgendaContext : DbContext
    {
        public AgendaContext() { }
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=agendawpf;Trusted_Connection=True");
            }
        }
    }
}
