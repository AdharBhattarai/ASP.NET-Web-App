using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResturantWebApp.Models;

namespace ResturantWebApp.Models
{
    public class DELMEContext : DbContext
    {
        public DELMEContext (DbContextOptions<DELMEContext> options)
            : base(options)
        {
        }

        public DbSet<ResturantWebApp.Models.Category> Category { get; set; }

        public DbSet<ResturantWebApp.Models.User> User { get; set; }

        public DbSet<ResturantWebApp.Models.Menu> Menu { get; set; }

        public DbSet<ResturantWebApp.Models.OrderStatus> OrderStatus { get; set; }

        public DbSet<ResturantWebApp.Models.MenuOrder> MenuOrder { get; set; }
    }
}
