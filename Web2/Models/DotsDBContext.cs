using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Web2
{
    public partial class DotsDBContext : DbContext
    {
        public DotsDBContext()
        {
        }

        public DotsDBContext(DbContextOptions<DotsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Map> Maps { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersMap> UsersMaps { get; set; }
        public virtual DbSet<UsersTag> UsersTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-VQNJPEO\\SQLEXPRESS;Initial Catalog=DotsDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Map>(entity =>
            {
                entity.HasIndex(e => e.MapId, "IX_MapID");

                entity.Property(e => e.MapId).HasColumnName("MapID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(35);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("ntext");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(e => e.NewId);

                entity.HasIndex(e => e.NewId, "IX_NewID");

                entity.Property(e => e.NewId).HasColumnName("NewID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.Titel)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK_News_TagID");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_RoleID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(35);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasIndex(e => e.TagId, "IX_TagID");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_ClientID");

                entity.HasIndex(e => e.Login, "UQ__Users__5E55825B56553ACC")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("email");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(35);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(35);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Users_RoleID");
            });

            modelBuilder.Entity<UsersMap>(entity =>
            {
                entity.HasIndex(e => e.UsersMapId, "IX_UsersMapID");

                entity.Property(e => e.UsersMapId).HasColumnName("UsersMapID");

                entity.Property(e => e.MapId).HasColumnName("MapID");

                entity.Property(e => e.Progress)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Map)
                    .WithMany(p => p.UsersMaps)
                    .HasForeignKey(d => d.MapId)
                    .HasConstraintName("FK_UsersMaps_MapID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersMaps)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UsersMaps_UserID");
            });

            modelBuilder.Entity<UsersTag>(entity =>
            {
                entity.HasIndex(e => e.UsersTagId, "IX_UsersTagID");

                entity.Property(e => e.UsersTagId).HasColumnName("UsersTagID");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.UsersTags)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK_UsersTags_TagID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersTags)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UsersTags_UserID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
