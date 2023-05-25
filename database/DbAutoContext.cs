using Microsoft.EntityFrameworkCore;
using ServiceStationApi.database.entities;

namespace ServiceStationApi.database;

public partial class DbAutoContext : DbContext
{
    public DbAutoContext()
    {
    }

    public DbAutoContext(DbContextOptions<DbAutoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarWork> CarWorks { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-29T6DCB\\SQLEXPRESS;Database=DB_Auto;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.IdAuto).HasName("DB_TableAuto_idAuto");

            entity.Property(e => e.IdAuto).HasColumnName("idAuto");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.NameAuto).HasMaxLength(70);
            entity.Property(e => e.VinCode)
                .HasMaxLength(20)
                .HasColumnName("VIN Code");
            entity.Property(e => e.YearsOfManufacture)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("Years of Manufacture");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Cars)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableAuto_TableUsers");
        });

        modelBuilder.Entity<CarWork>(entity =>
        {
            entity.HasKey(e => e.IdWork).HasName("PK_TableWork");

            entity.Property(e => e.IdWork).HasColumnName("idWork");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.DescriptionWork)
                .HasMaxLength(250)
                .HasColumnName("Description Work");
            entity.Property(e => e.IdAuto).HasColumnName("idAuto");
            entity.Property(e => e.Note).HasMaxLength(500);
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("DB_TableUsers_idUser");

            entity.HasIndex(e => e.NameOwner, "DB_TableUsers_NameOwner").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.NameOwner).HasMaxLength(70);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
