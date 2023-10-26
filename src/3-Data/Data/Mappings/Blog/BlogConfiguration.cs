using Business.Models.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings.Blog;

public class BlogConfiguration : IEntityTypeConfiguration<BlogEntity>
{
    public void Configure(EntityTypeBuilder<BlogEntity> builder)
    {
        builder.ToTable("blogs");

        builder.HasKey(e => e.Id);

        builder.Property(rb => rb.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(rb => rb.NormalizedName)
            .HasColumnName("normalized_name")
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(rb => rb.Name).IsUnique();
        builder.HasIndex(rb => rb.NormalizedName).IsUnique();

        builder.HasMany(be => be.Users)
                .WithMany(ub => ub.Blogs);

        builder.HasMany(be => be.RecipePosts)
            .WithOne(mp => mp.Blog)
            .HasForeignKey(mp => mp.BlogId);

        builder.HasMany(be => be.Categories)
            .WithOne(c => c.Blog)
            .HasForeignKey(c => c.BlogId);
        //.OnDelete(DeleteBehavior.NoAction);
    }
}
