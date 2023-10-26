using Microsoft.EntityFrameworkCore;


namespace Example_App.Infraestructura.Model
{
    public partial class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> user { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdControl).HasName("idControl");

                entity.ToTable("aspirantes");

                entity.Property(e => e.IdControl).HasColumnName("idControl");
                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Address)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.Age)
                    .HasMaxLength(12)
                    .IsUnicode(false);
                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .IsUnicode(false);
                entity.Property(e => e.Enabled)
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });



            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }

}
