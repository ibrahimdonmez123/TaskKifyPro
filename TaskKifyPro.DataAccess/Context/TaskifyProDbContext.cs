using TaskKifyPro.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskKifyPro.DataAccess.Context
{
    public class TaskifyProDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=185.8.129.235;Database=Taskify360DB;User Id=ibrahim;Password=H8!37h3fn;Encrypt=True;TrustServerCertificate=True;");

        }
     
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Duty> Duties { get; set; }
        public DbSet<UserDuty> UserDuties { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Notification> Notifications { get; set; }




    }
}
