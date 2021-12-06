using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Songify.Simple.Models;

namespace Songify.Simple.DAL
{
    public class SongifyDbContext : DbContext, IUnitOfWork
    {
        
        public DbSet<Artist> Artists { get; set; }

        public SongifyDbContext(DbContextOptions<SongifyDbContext> options) : base(options)
        {
        }
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            // Use sensitive login
            //optionsBuilder.us
        }
    
        private static readonly ILoggerFactory MyLoggerFactory = 
            LoggerFactory.Create(builder => { builder.AddConsole(); });
        
    }
}