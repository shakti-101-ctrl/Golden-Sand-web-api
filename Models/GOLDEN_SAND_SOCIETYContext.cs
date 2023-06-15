using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GoldenSand_WebAPI.Models
{
    public partial class GOLDEN_SAND_SOCIETYContext : DbContext
    {
        public GOLDEN_SAND_SOCIETYContext()
        {
        }

        public GOLDEN_SAND_SOCIETYContext(DbContextOptions<GOLDEN_SAND_SOCIETYContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DuplexDetails> DuplexDetails { get; set; }
        public virtual DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public virtual DbSet<EventsDetails> EventsDetails { get; set; }
        public virtual DbSet<ExpenseDetails> ExpenseDetails { get; set; }
        public virtual DbSet<IncomeDetails> IncomeDetails { get; set; }
        public virtual DbSet<MeetingDetails> MeetingDetails { get; set; }
        public virtual DbSet<NoticeDetails> NoticeDetails { get; set; }
        public virtual DbSet<NoticeDetailsTrash> NoticeDetailsTrash { get; set; }
        public virtual DbSet<PropertyDeclaration> PropertyDeclaration { get; set; }
        public virtual DbSet<TenantDetails> TenantDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DuplexDetails>(entity =>
            {
                entity.HasKey(e => e.DuplexId);

                entity.ToTable("DUPLEX_DETAILS", "AD");

                entity.Property(e => e.DuplexId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AlternateContact)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Contact)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.DeleteStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.DuplexNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .HasColumnName("EmailID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OwnerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmployeeDetails>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("EMPLOYEE_DETAILS", "AD");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressDetails)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Contact)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.FatherName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.JoiningDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Position)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderAddressdetails)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderAlternateContact)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderContact)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderOwnerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EventsDetails>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("EVENTS_DETAILS", "AD");

                entity.Property(e => e.EventId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.DeleteStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.EventEndDate).HasColumnType("datetime");

                entity.Property(e => e.EventStartDate).HasColumnType("datetime");

                entity.Property(e => e.HeadingText)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PostedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ExpenseDetails>(entity =>
            {
                entity.HasKey(e => e.ExpenseId);

                entity.ToTable("EXPENSE_DETAILS", "AD");

                entity.Property(e => e.ExpenseId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DateEntry).HasColumnType("datetime");

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.ExpenseHead)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Narration)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TowhomOrTransactionId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IncomeDetails>(entity =>
            {
                entity.HasKey(e => e.IncomeId);

                entity.ToTable("INCOME_DETAILS", "AD");

                entity.Property(e => e.IncomeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DateEntry).HasColumnType("datetime");

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.DuplexNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Narration)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Purpose)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TowhomOrTransactionId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.DuplexNumberNavigation)
                    .WithMany(p => p.IncomeDetails)
                    .HasForeignKey(d => d.DuplexNumber)
                    .HasConstraintName("FK_duplexdetails_INCOMEDETAILS");
            });

            modelBuilder.Entity<MeetingDetails>(entity =>
            {
                entity.HasKey(e => e.MeetingId);

                entity.ToTable("MEETING_DETAILS", "AD");

                entity.Property(e => e.MeetingId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.DeleteStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.HeadingText)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.InvitedPersons).IsUnicode(false);

                entity.Property(e => e.LasteModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MeetEndDateTime).HasColumnType("datetime");

                entity.Property(e => e.MeetStartDateTime).HasColumnType("datetime");

                entity.Property(e => e.PostedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<NoticeDetails>(entity =>
            {
                entity.HasKey(e => e.NoticeId);

                entity.ToTable("NOTICE_DETAILS", "AD");

                entity.Property(e => e.NoticeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.DeleteStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.HeadingText)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PostedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<NoticeDetailsTrash>(entity =>
            {
                entity.HasKey(e => e.NoticeId);

                entity.ToTable("NOTICE_DETAILS_TRASH", "AD");

                entity.Property(e => e.NoticeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.HeadingText)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PostedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PropertyDeclaration>(entity =>
            {
                entity.HasKey(e => e.PropertyId);

                entity.ToTable("PROPERTY_DECLARATION", "AD");

                entity.Property(e => e.PropertyId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NameOfProperty)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyDescription)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TenantDetails>(entity =>
            {
                entity.HasKey(e => e.TenantId);

                entity.ToTable("TENANT_DETAILS", "AD");

                entity.Property(e => e.TenantId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.DuplexNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Occupation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StayingDate).HasColumnType("datetime");

                entity.Property(e => e.TenantType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.DuplexNumberNavigation)
                    .WithMany(p => p.TenantDetails)
                    .HasForeignKey(d => d.DuplexNumber)
                    .HasConstraintName("FK_DUPLEX_DETAILS_TENANT_DETAILS");
            });
        }
    }
}
