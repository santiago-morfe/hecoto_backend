using Hecoto.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Hecoto.Backend.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RefreshToken>()
            .HasOne(rt => rt.User) // Relación con la entidad User
            .WithMany(u => u.RefreshTokens) // Un usuario puede tener muchos RefreshTokens
            .HasForeignKey(rt => rt.UserId) // Clave foránea en RefreshToken
            .OnDelete(DeleteBehavior.Cascade); // Eliminar en cascada
    }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=hecoto_db;User Id=postgres;Password=1234;");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}