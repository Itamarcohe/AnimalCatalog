using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetCatalog.Models;
namespace PetCatalog.Data


{
    public class PetContext : IdentityDbContext
    {
        public PetContext(DbContextOptions<PetContext> options) : base(options) { }

        virtual public DbSet<Animal>? Animals { get; set; }
        virtual public DbSet<AnimalCategory>? AnimalCategories { get; set; }
        virtual public DbSet<Comment>? Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(d => d.Animal).WithMany(p => p.Comments)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comments_Animals");
            });


            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(); 
        }

    }
}
