using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PRN212.Models;

public partial class Prn212Context : DbContext
{
    public Prn212Context()
    {
    }

    public Prn212Context(DbContextOptions<Prn212Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<Violation> Violations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        
        optionsBuilder.UseSqlServer(config.GetConnectionString("default"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E32AB7A643E");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.Message).HasColumnType("text");
            entity.Property(e => e.PlateNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.SentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.PlateNumberNavigation).WithMany(p => p.Notifications)
                .HasPrincipalKey(p => p.PlateNumber)
                .HasForeignKey(d => d.PlateNumber)
                .HasConstraintName("FK__Notificat__Plate__46E78A0C");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__UserI__47DBAE45");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Reports__D5BD48E50602BB45");

            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ImageUrl)
                .HasColumnType("text")
                .HasColumnName("ImageURL");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlateNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ReportDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReporterId).HasColumnName("ReporterID");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("Pending");
            entity.Property(e => e.VideoUrl)
                .HasColumnType("text")
                .HasColumnName("VideoURL");
            entity.Property(e => e.ViolationType)
                .HasMaxLength(50)
                .IsUnicode(false);
            
            entity.HasOne(d => d.ProcessedByNavigation).WithMany(p => p.ReportProcessedByNavigations)
                .HasForeignKey(d => d.ProcessedBy)
                .HasConstraintName("FK__Reports__Process__49C3F6B7");

            entity.HasOne(d => d.Reporter).WithMany(p => p.ReportReporters)
                .HasForeignKey(d => d.ReporterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reports__Reporte__4AB81AF0");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACD95F86EC");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053434F8B744").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicles__476B54B23CD5AA59");

            entity.HasIndex(e => e.PlateNumber, "UQ__Vehicles__03692624E860A32E").IsUnique();

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.PlateNumber)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Owner).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehicles__OwnerI__4BAC3F29");
        });

        modelBuilder.Entity<Violation>(entity =>
        {
            entity.HasKey(e => e.ViolationId).HasName("PK__Violatio__18B6DC282676E154");

            entity.Property(e => e.ViolationId).HasColumnName("ViolationID");
            entity.Property(e => e.FineAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FineDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaidStatus).HasDefaultValue(false);
            entity.Property(e => e.PlateNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.ViolatorId).HasColumnName("ViolatorID");

            entity.HasOne(d => d.PlateNumberNavigation).WithMany(p => p.Violations)
                .HasPrincipalKey(p => p.PlateNumber)
                .HasForeignKey(d => d.PlateNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Violation__Plate__4CA06362");

            entity.HasOne(d => d.Report).WithMany(p => p.Violations)
                .HasForeignKey(d => d.ReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Violation__Repor__4D94879B");

            entity.HasOne(d => d.Violator).WithMany(p => p.Violations)
                .HasForeignKey(d => d.ViolatorId)
                .HasConstraintName("FK__Violation__Viola__4E88ABD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
