using Business.Models.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Data.Mappings.Blog;

public class UserBlogConfiguration : IEntityTypeConfiguration<UserBlog>
{
    public void Configure(EntityTypeBuilder<UserBlog> builder)
    {
        builder.ToTable("user_blogs");

        builder.HasKey(e => e.Id);

        builder.Property(rb => rb.UserId)
               .HasColumnName("user_id")
               .IsRequired();

        builder.Property(rb => rb.BlogId)
               .HasColumnName("blog_id")
               .IsRequired();

        builder.HasOne(ub => ub.User)
               .WithMany(u => u.Blogs)
               .HasForeignKey(ub => ub.UserId);

        builder.HasOne(ub => ub.Blog)
               .WithMany(b => b.Users)
               .HasForeignKey(ub => ub.BlogId);
    }
}
