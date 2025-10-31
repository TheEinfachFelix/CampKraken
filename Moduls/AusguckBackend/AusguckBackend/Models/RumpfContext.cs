using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AusguckBackend.Models;

public partial class RumpfContext : DbContext
{
    public RumpfContext()
    {
    }

    public RumpfContext(DbContextOptions<RumpfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<ContactInfo> ContactInfos { get; set; }

    public virtual DbSet<ContactInfoType> ContactInfoTypes { get; set; }

    public virtual DbSet<Day> Days { get; set; }

    public virtual DbSet<DiscountCode> DiscountCodes { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Nutrition> Nutritions { get; set; }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<ParticipantsPrivate> ParticipantsPrivates { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<SchoolType> SchoolTypes { get; set; }

    public virtual DbSet<ShirtSize> ShirtSizes { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(Globals.ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("addresses_pkey");

            entity.ToTable("addresses");

            entity.Property(e => e.AddressId).HasColumnName("addressId");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.CoverName).HasColumnName("coverName");
            entity.Property(e => e.PersonId).HasColumnName("personId");
            entity.Property(e => e.StreetAndNumber).HasColumnName("streetAndNumber");
            entity.Property(e => e.ZipCode).HasColumnName("zipCode");

            entity.HasOne(d => d.Person).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("addresses_personid_person_personid_fkey");
        });

        modelBuilder.Entity<ContactInfo>(entity =>
        {
            entity.HasKey(e => e.ContactInfoId).HasName("contactInfo_pkey");

            entity.ToTable("contactInfo");

            entity.Property(e => e.ContactInfoId).HasColumnName("contactInfoId");
            entity.Property(e => e.ContactInfoTypeId).HasColumnName("contactInfoTypeId");
            entity.Property(e => e.Details).HasColumnName("details");
            entity.Property(e => e.Info).HasColumnName("info");
            entity.Property(e => e.PersonId).HasColumnName("personId");

            entity.HasOne(d => d.ContactInfoType).WithMany(p => p.ContactInfos)
                .HasForeignKey(d => d.ContactInfoTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("contactinfo_contactinfotypeid_contactinfotypes_contactinfotypei");

            entity.HasOne(d => d.Person).WithMany(p => p.ContactInfos)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("contactinfo_personid_person_personid_fkey");
        });

        modelBuilder.Entity<ContactInfoType>(entity =>
        {
            entity.HasKey(e => e.ContactInfoTypeId).HasName("contactInfoTypes_pkey");

            entity.ToTable("contactInfoTypes");

            entity.Property(e => e.ContactInfoTypeId).HasColumnName("contactInfoTypeId");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Day>(entity =>
        {
            entity.HasKey(e => e.DayId).HasName("days_pkey");

            entity.ToTable("days");

            entity.Property(e => e.DayId).HasColumnName("dayId");
            entity.Property(e => e.Date).HasColumnName("date");
        });

        modelBuilder.Entity<DiscountCode>(entity =>
        {
            entity.HasKey(e => e.DiscountCodeId).HasName("discountCodes_pkey");

            entity.ToTable("discountCodes");

            entity.Property(e => e.DiscountCodeId).HasColumnName("discountCodeId");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("genders_pkey");

            entity.ToTable("genders");

            entity.Property(e => e.GenderId).HasColumnName("genderId");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Nutrition>(entity =>
        {
            entity.HasKey(e => e.NutritionId).HasName("nutritions_pkey");

            entity.ToTable("nutritions");

            entity.Property(e => e.NutritionId).HasColumnName("nutritionId");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasKey(e => e.ParticipantId).HasName("participants_pkey");

            entity.ToTable("participants");

            entity.Property(e => e.ParticipantId).HasColumnName("participantId");
            entity.Property(e => e.CancelationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cancelationDate");
            entity.Property(e => e.ConfirmationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("confirmationDate");
            entity.Property(e => e.DiscountCodeId).HasColumnName("discountCodeId");
            entity.Property(e => e.PersonId).HasColumnName("personId");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("registrationDate");
            entity.Property(e => e.ReminderDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("reminderDate");
            entity.Property(e => e.SelectedSlot).HasColumnName("selectedSlot");
            entity.Property(e => e.ShirtSizeId).HasColumnName("shirtSizeId");
            entity.Property(e => e.UserDiscountCode).HasColumnName("userDiscountCode");

            entity.HasOne(d => d.DiscountCode).WithMany(p => p.Participants)
                .HasForeignKey(d => d.DiscountCodeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("participants_discountcodeid_discountcodes_discountcodeid_fkey");

            entity.HasOne(d => d.Person).WithMany(p => p.Participants)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("participants_personid_person_personid_fkey");

            entity.HasOne(d => d.ShirtSize).WithMany(p => p.Participants)
                .HasForeignKey(d => d.ShirtSizeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("participants_shirtsizeid_shirtsizes_shirtsizeid_fkey");

            entity.HasMany(d => d.Tags).WithMany(p => p.Participants)
                .UsingEntity<Dictionary<string, object>>(
                    "TagToParticipant",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("tagtoparticipant_tagid_tags_tagid_fkey"),
                    l => l.HasOne<Participant>().WithMany()
                        .HasForeignKey("ParticipantId")
                        .HasConstraintName("tagtoparticipant_participantid_participants_participantid_fkey"),
                    j =>
                    {
                        j.HasKey("ParticipantId", "TagId").HasName("tagToParticipant_pkey");
                        j.ToTable("tagToParticipant");
                        j.IndexerProperty<int>("ParticipantId").HasColumnName("participantId");
                        j.IndexerProperty<int>("TagId").HasColumnName("tagId");
                    });
        });

        modelBuilder.Entity<ParticipantsPrivate>(entity =>
        {
            entity.HasKey(e => e.ParticipantId).HasName("participantsPrivate_pkey");

            entity.ToTable("participantsPrivate");

            entity.Property(e => e.ParticipantId)
                .ValueGeneratedOnAdd()
                .HasColumnName("participantId");
            entity.Property(e => e.Doctor).HasColumnName("doctor");
            entity.Property(e => e.HealthInfo).HasColumnName("healthInfo");
            entity.Property(e => e.InsuredBy).HasColumnName("insuredBy");
            entity.Property(e => e.Intolerances).HasColumnName("intolerances");
            entity.Property(e => e.NutritionId).HasColumnName("nutritionId");
            entity.Property(e => e.SchoolTypeId).HasColumnName("schoolTypeId");
            entity.Property(e => e.SpecialInfos).HasColumnName("specialInfos");

            entity.HasOne(d => d.Nutrition).WithMany(p => p.ParticipantsPrivates)
                .HasForeignKey(d => d.NutritionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("participantsprivate_nutritionid_nutritions_nutritionid_fkey");

            entity.HasOne(d => d.Participant).WithOne(p => p.ParticipantsPrivate)
                .HasForeignKey<ParticipantsPrivate>(d => d.ParticipantId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("participantsprivate_participantid_participants_participantid_fk");

            entity.HasOne(d => d.SchoolType).WithMany(p => p.ParticipantsPrivates)
                .HasForeignKey(d => d.SchoolTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("participantsprivate_schooltypeid_schooltypes_schooltypeid_fkey");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("person_pkey");

            entity.ToTable("person");

            entity.Property(e => e.PersonId).HasColumnName("personId");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.FirstName).HasColumnName("firstName");
            entity.Property(e => e.GenderId).HasColumnName("genderId");
            entity.Property(e => e.LastName).HasColumnName("lastName");

            entity.HasOne(d => d.Gender).WithMany(p => p.People)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("person_genderid_genders_genderid_fkey");

            entity.HasMany(d => d.Days).WithMany(p => p.People)
                .UsingEntity<Dictionary<string, object>>(
                    "Presence",
                    r => r.HasOne<Day>().WithMany()
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("presences_dayid_days_dayid_fkey"),
                    l => l.HasOne<Person>().WithMany()
                        .HasForeignKey("PersonId")
                        .HasConstraintName("presences_personid_person_personid_fkey"),
                    j =>
                    {
                        j.HasKey("PersonId", "DayId").HasName("presences_pkey");
                        j.ToTable("presences");
                        j.IndexerProperty<int>("PersonId").HasColumnName("personId");
                        j.IndexerProperty<int>("DayId").HasColumnName("dayId");
                    });
        });

        modelBuilder.Entity<SchoolType>(entity =>
        {
            entity.HasKey(e => e.SchoolTypeId).HasName("schoolTypes_pkey");

            entity.ToTable("schoolTypes");

            entity.Property(e => e.SchoolTypeId).HasColumnName("schoolTypeId");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<ShirtSize>(entity =>
        {
            entity.HasKey(e => e.ShirtSizeId).HasName("shirtSizes_pkey");

            entity.ToTable("shirtSizes");

            entity.Property(e => e.ShirtSizeId).HasColumnName("shirtSizeId");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("staff_pkey");

            entity.ToTable("staff");

            entity.Property(e => e.StaffId).HasColumnName("staffId");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.PersonId).HasColumnName("personId");
            entity.Property(e => e.UserName).HasColumnName("userName");

            entity.HasOne(d => d.Person).WithMany(p => p.Staff)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("staff_personid_person_personid_fkey");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("tags_pkey");

            entity.ToTable("tags");

            entity.Property(e => e.TagId).HasColumnName("tagId");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
