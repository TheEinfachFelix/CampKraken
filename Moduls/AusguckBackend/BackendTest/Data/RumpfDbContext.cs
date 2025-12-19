using System;
using System.Collections.Generic;
using AusguckBackend;
using BackendTest.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Data;

public partial class RumpfDbContext : DbContext
{
    public RumpfDbContext()
    {
    }

    public RumpfDbContext(DbContextOptions<RumpfDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<address> addresses { get; set; }

    public virtual DbSet<contactInfo> contactInfos { get; set; }

    public virtual DbSet<contactInfoType> contactInfoTypes { get; set; }

    public virtual DbSet<day> days { get; set; }

    public virtual DbSet<discountCode> discountCodes { get; set; }

    public virtual DbSet<gender> genders { get; set; }

    public virtual DbSet<nutrition> nutritions { get; set; }

    public virtual DbSet<participant> participants { get; set; }

    public virtual DbSet<participantsPrivate> participantsPrivates { get; set; }

    public virtual DbSet<person> people { get; set; }

    public virtual DbSet<schoolType> schoolTypes { get; set; }

    public virtual DbSet<shirtSize> shirtSizes { get; set; }

    public virtual DbSet<staff> staff { get; set; }

    public virtual DbSet<tag> tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(Globals.TestConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<address>(entity =>
        {
            entity.HasKey(e => e.addressId).HasName("addresses_pkey");

            entity.HasOne(d => d.person).WithMany(p => p.addresses).HasConstraintName("addresses_personid_person_personid_fkey");
        });

        modelBuilder.Entity<contactInfo>(entity =>
        {
            entity.HasKey(e => e.contactInfoId).HasName("contactInfo_pkey");

            entity.HasOne(d => d.contactInfoType).WithMany(p => p.contactInfos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("contactinfo_contactinfotypeid_contactinfotypes_contactinfotypei");

            entity.HasOne(d => d.person).WithMany(p => p.contactInfos).HasConstraintName("contactinfo_personid_person_personid_fkey");
        });

        modelBuilder.Entity<contactInfoType>(entity =>
        {
            entity.HasKey(e => e.contactInfoTypeId).HasName("contactInfoTypes_pkey");
        });

        modelBuilder.Entity<day>(entity =>
        {
            entity.HasKey(e => e.dayId).HasName("days_pkey");
        });

        modelBuilder.Entity<discountCode>(entity =>
        {
            entity.HasKey(e => e.discountCodeId).HasName("discountCodes_pkey");
        });

        modelBuilder.Entity<gender>(entity =>
        {
            entity.HasKey(e => e.genderId).HasName("genders_pkey");
        });

        modelBuilder.Entity<nutrition>(entity =>
        {
            entity.HasKey(e => e.nutritionId).HasName("nutritions_pkey");

            entity.HasMany(d => d.participants).WithMany(p => p.nutritions)
                .UsingEntity<Dictionary<string, object>>(
                    "nutritionsToPrivate",
                    r => r.HasOne<participantsPrivate>().WithMany()
                        .HasForeignKey("participantId")
                        .HasConstraintName("nutritionstoprivate_participantid_participantsprivate_participa"),
                    l => l.HasOne<nutrition>().WithMany()
                        .HasForeignKey("nutritionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("nutritionstoprivate_nutritionid_nutritions_nutritionid_fkey"),
                    j =>
                    {
                        j.HasKey("nutritionId", "participantId").HasName("nutritionsToPrivate_pkey");
                        j.ToTable("nutritionsToPrivate");
                    });
        });

        modelBuilder.Entity<participant>(entity =>
        {
            entity.HasKey(e => e.participantId).HasName("participants_pkey");

            entity.Property(e => e.registrationDate).HasDefaultValueSql("now()");

            entity.HasOne(d => d.discountCode).WithMany(p => p.participants)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("participants_discountcodeid_discountcodes_discountcodeid_fkey");

            entity.HasOne(d => d.person).WithMany(p => p.participants)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("participants_personid_person_personid_fkey");

            entity.HasOne(d => d.shirtSize).WithMany(p => p.participants)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("participants_shirtsizeid_shirtsizes_shirtsizeid_fkey");

            entity.HasMany(d => d.tags).WithMany(p => p.participants)
                .UsingEntity<Dictionary<string, object>>(
                    "tagToParticipant",
                    r => r.HasOne<tag>().WithMany()
                        .HasForeignKey("tagId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("tagtoparticipant_tagid_tags_tagid_fkey"),
                    l => l.HasOne<participant>().WithMany()
                        .HasForeignKey("participantId")
                        .HasConstraintName("tagtoparticipant_participantid_participants_participantid_fkey"),
                    j =>
                    {
                        j.HasKey("participantId", "tagId").HasName("tagToParticipant_pkey");
                        j.ToTable("tagToParticipant");
                    });
        });

        modelBuilder.Entity<participantsPrivate>(entity =>
        {
            entity.HasKey(e => e.participantId).HasName("participantsPrivate_pkey");

            entity.Property(e => e.participantId).ValueGeneratedNever();

            entity.HasOne(d => d.participant).WithOne(p => p.participantsPrivate)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("participantsprivate_participantid_participants_participantid_fk");

            entity.HasOne(d => d.schoolType).WithMany(p => p.participantsPrivates)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("participantsprivate_schooltypeid_schooltypes_schooltypeid_fkey");
        });

        modelBuilder.Entity<person>(entity =>
        {
            entity.HasKey(e => e.personId).HasName("person_pkey");

            entity.HasOne(d => d.gender).WithMany(p => p.people)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("person_genderid_genders_genderid_fkey");

            entity.HasMany(d => d.days).WithMany(p => p.people)
                .UsingEntity<Dictionary<string, object>>(
                    "presence",
                    r => r.HasOne<day>().WithMany()
                        .HasForeignKey("dayId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("presences_dayid_days_dayid_fkey"),
                    l => l.HasOne<person>().WithMany()
                        .HasForeignKey("personId")
                        .HasConstraintName("presences_personid_person_personid_fkey"),
                    j =>
                    {
                        j.HasKey("personId", "dayId").HasName("presences_pkey");
                        j.ToTable("presences");
                    });
        });

        modelBuilder.Entity<schoolType>(entity =>
        {
            entity.HasKey(e => e.schoolTypeId).HasName("schoolTypes_pkey");
        });

        modelBuilder.Entity<shirtSize>(entity =>
        {
            entity.HasKey(e => e.shirtSizeId).HasName("shirtSizes_pkey");
        });

        modelBuilder.Entity<staff>(entity =>
        {
            entity.HasKey(e => e.staffId).HasName("staff_pkey");

            entity.HasOne(d => d.person).WithMany(p => p.staff)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("staff_personid_person_personid_fkey");
        });

        modelBuilder.Entity<tag>(entity =>
        {
            entity.HasKey(e => e.tagId).HasName("tags_pkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
