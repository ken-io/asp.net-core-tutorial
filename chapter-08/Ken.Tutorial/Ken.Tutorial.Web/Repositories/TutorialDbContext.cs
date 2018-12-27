using System;
using Ken.Tutorial.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ken.Tutorial.Web.Repositories
{
    public class TutorialDbContext : DbContext
    {
        private IConfiguration Configuration { get; }

        public TutorialDbContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Configuration.GetConnectionString("testdb"));
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}