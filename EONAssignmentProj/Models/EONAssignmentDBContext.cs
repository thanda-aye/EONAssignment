using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EONAssignmentProj.Models
{
    public partial class EONAssignmentDBContext : DbContext
    {
        //public EONAssignmentDBContext()
        //{
        //}

        public EONAssignmentDBContext(DbContextOptions<EONAssignmentDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserTbl> UserTbls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=THANDA\\SQLEXPRESS;Database=EONAssignmentDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTbl>(entity =>
            {
                entity.ToTable("User_tbl");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddRequest)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AreaOfInterest)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DateReg)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SelectedDays)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
