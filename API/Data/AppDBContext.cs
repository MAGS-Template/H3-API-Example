﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DomainModels;

namespace H3_PostgresRESTFulAPI.Data
{
    public class AppDBContext : DbContext
    {
        protected readonly IConfiguration configuration;

        public AppDBContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RGBData> RGBData { get; set; }
        public DbSet<UserDTO> UserDTO { get; set; } = default!;
    }
}
