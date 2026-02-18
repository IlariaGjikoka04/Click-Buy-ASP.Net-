using Click_Buy1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Click_Buy1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Product1> Products { get; set; } = null!;
        public DbSet<Jackets> Jackets { get; set; } = null!;
        public DbSet<Jeans> Jeans { get; set; } = null!;
        public DbSet<Trousers> Trousers { get; set; } = null!;
        public DbSet<SweatersANDcardigans> SweatersANDcardigans { get; set; } = null!;
        public DbSet<Tshirts> Tshirts { get; set; } = null!;
        public DbSet<AddToCart> AddToCart { get; set; } = null!;

        public DbSet<NewCollection> NewCollection { get; set; } = null!;
        public DbSet<Orders> Orders { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Specifiko precision dhe scale për kolonën Price
            modelBuilder.Entity<Product1>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Jeans>()
               .Property(j => j.Price)
               .HasPrecision(18, 2);
            modelBuilder.Entity<SweatersANDcardigans>()
              .Property(s => s.Price)
              .HasPrecision(18, 2);
            modelBuilder.Entity<Trousers>()
            .Property(t => t.Price)
            .HasPrecision(18, 2);
            modelBuilder.Entity<Tshirts>()
           .Property(ts => ts.Price)
           .HasPrecision(18, 2);
            modelBuilder.Entity<Jackets>()
          .Property(jk => jk.Price)
          .HasPrecision(18, 2);
            modelBuilder.Entity<NewCollection>()
         .Property(c => c.Price)
         .HasPrecision(18, 2);
        }
        public DbSet<Click_Buy1.Models.AspNetUsersViewModel>? AspNetUsersViewModel { get; set; }
    }
}
