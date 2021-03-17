using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPEntityFramework.EF
{
    class NorthwindContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-SN2MF5L;Initial Catalog=Northwind;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
