using DomainTables;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public DbSet<Operation> Operations => Set<Operation>();
        public DbSet<Detail> Details => Set<Detail>();
        public DbSet<Production> Productions => Set<Production>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=details.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Detail>()
                .Property(d => d.CodeDetail)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Production>()
                .HasKey(p => new { p.CodeDetail, p.OperationNumber });

            modelBuilder.Entity<Production>()
                .HasOne(p => p.Detail)
                .WithMany(d => d.Productions)
                .HasForeignKey(p => p.CodeDetail)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Production>()
                .HasOne(p => p.Operation)
                .WithMany()
                .HasForeignKey(p => p.CodeOperation)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
