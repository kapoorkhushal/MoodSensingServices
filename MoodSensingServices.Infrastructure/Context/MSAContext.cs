using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MoodSensingServices.Application.Entities;

namespace MoodSensingServices.Infrastructure.Context;

public partial class MSAContext : DbContext
{
    public MSAContext()
    {
    }

    public MSAContext(DbContextOptions<MSAContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=IN-910D5S3; Database=MoodSensing; Integrated Security=SSPI; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.LocationId, "IX_User_LocationId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Location).WithMany(p => p.Users).HasForeignKey(d => d.LocationId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
