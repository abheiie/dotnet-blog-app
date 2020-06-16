using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectC.Models
{
    public partial class DatabaseCContext : DbContext
    {
        public DatabaseCContext()
        {
        }

        public DatabaseCContext(DbContextOptions<DatabaseCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=DatabaseC;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnName("body")
                    .HasMaxLength(2000)
                    .IsFixedLength();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(200)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.TagId).HasColumnName("tagId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Tag)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tag_Post");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
