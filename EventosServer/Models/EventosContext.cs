using Microsoft.EntityFrameworkCore;

namespace EventosServer.Models
{
    public class EventosContext : DbContext
    {
        public EventosContext(DbContextOptions<EventosContext> options) : base(options){ 
        }

        public DbSet<Evento> Eventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Evento>().HasIndex(c => c.Id).IsUnique();
        }
    }
}
