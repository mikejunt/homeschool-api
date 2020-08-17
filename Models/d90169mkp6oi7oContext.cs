using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace homeschool_api.Models
{
    public partial class d90169mkp6oi7oContext : DbContext
    {
        public d90169mkp6oi7oContext()
        {
        }

        public d90169mkp6oi7oContext(DbContextOptions<d90169mkp6oi7oContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Family> Family { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<UserToFamily> UserToFamily { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Family>(entity =>
            {
                entity.ToTable("family");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AdminId).HasColumnName("admin id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(35);

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Family)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("admin id to userid");
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.ToTable("tasks");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AssigneeId).HasColumnName("assignee id");

                entity.Property(e => e.AuthorId).HasColumnName("author id");

                entity.Property(e => e.Completed).HasColumnName("completed");

                entity.Property(e => e.Completetime).HasColumnName("completetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(100);

                entity.Property(e => e.Duetime).HasColumnName("duetime");

                entity.Property(e => e.FamilyId).HasColumnName("family id");

                entity.Property(e => e.Photo)
                    .HasColumnName("photo")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Assignee)
                    .WithMany(p => p.TasksAssignee)
                    .HasForeignKey(d => d.AssigneeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("assignee id to userid");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.TasksAuthor)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("author id to userid");

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("family id to family");
            });

            modelBuilder.Entity<UserToFamily>(entity =>
            {
                entity.ToTable("user to family");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Confirmed).HasColumnName("confirmed");

                entity.Property(e => e.FamilyId).HasColumnName("family id");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.UserId).HasColumnName("user id");

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.UserToFamily)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("family id to family");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserToFamily)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user id to user");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("email unique identifier")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(30);

                entity.Property(e => e.Minor).HasColumnName("minor");

                entity.Property(e => e.ParentEmail)
                    .HasColumnName("parent_email")
                    .HasMaxLength(100);

                entity.Property(e => e.Photo)
                    .HasColumnName("photo")
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
