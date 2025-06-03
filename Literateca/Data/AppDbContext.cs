using System.Reflection;
using Literateca.Data.Mappings;
using Literateca.Models;
using Microsoft.EntityFrameworkCore;

namespace Literateca.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Livro> Livros { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LivroMapping());
    }
}