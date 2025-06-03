using Literateca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Literateca.Data.Mappings;

public class LivroMapping : IEntityTypeConfiguration<Livro>
{
    public void Configure(EntityTypeBuilder<Livro> builder)
    {
        builder.ToTable("Livro");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Titulo)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Autor)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Genero)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Preco)
            .HasColumnType("DECIMAL");

        builder.Property(x => x.Quantidade)
            .IsRequired()
            .HasColumnType("INT");
    }
}