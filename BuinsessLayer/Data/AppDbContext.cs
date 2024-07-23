using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace BuisnessLayer.Data
{
    public class AppDbContext : IdentityDbContext
    {



        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
       
        public DbSet<CreateBookViewModel> CreateBooks
        {
            get; set;
        }
        public DbSet<CommentViewModel> Comments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            string connectionString = "Server=.;Database=MVCProject;MultipleActiveResultSets=True;Trusted_Connection=True;"; 
            optionsBuilder.UseSqlServer
                (connectionString, options => {
                    options.MigrationsAssembly("BookStore"); 
                }
                );
        }
    }
}

