using EcfCdaDotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace EcfCdaDotNet.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<Participation> Participations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=MyDbConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Event");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("End_Date");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("Start_Date");
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.ToTable("Participant");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        modelBuilder.Entity<Participation>(entity =>
        {
            entity.HasKey(e => new { e.IdParticipant, e.IdEvent });

            entity.ToTable("Participation");

            entity.Property(e => e.IdParticipant).HasColumnName("Id_Participant");
            entity.Property(e => e.IdEvent).HasColumnName("Id_Event");
            entity.Property(e => e.RegistrationDate)
                .HasColumnType("datetime")
                .HasColumnName("Registration_Date");

            entity.HasOne(d => d.IdEventNavigation).WithMany(p => p.Participations)
                .HasForeignKey(d => d.IdEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Participation_Event");

            entity.HasOne(d => d.IdParticipantNavigation).WithMany(p => p.Participations)
                .HasForeignKey(d => d.IdParticipant)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Participation_Participant");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
