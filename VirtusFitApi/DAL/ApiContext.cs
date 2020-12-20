using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtusFitApi.Models;

namespace VirtusFitApi.DAL
{
    public class ApiContext : DbContext
    {
        private static readonly string ConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=VirtusFitApi;Trusted_Connection=True;";

        public DbSet<ProductAction> ProductActions { get; set; }
        public DbSet<DietPlanAction> DietPlanActions { get; set; }
        public DbSet<ProductInPlanAction> ProductInPlanActions { get; set; }

        public DbSet<SearchValueAction> SearchValueActions { get; set; }
        public DbSet<SearchStringAction> SearchStringActions { get; set; }
        public DbSet<BmiAction> BmiActions { get; set; }
        public DbSet<UserAccountAction> UserAccountActions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
