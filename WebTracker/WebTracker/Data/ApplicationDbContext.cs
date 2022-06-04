using System;
using Microsoft.EntityFrameworkCore;
using WebTracker.Models;

namespace WebTracker.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Website> Websites { get; set; }
        public DbSet<Flow> Flows { get; set; }
        public DbSet<Models.Action> Actions { get; set; }
        public DbSet<ActionData> ActionsData { get; set; }
        public DbSet <Url> Urls { get; set; }
    }
}
