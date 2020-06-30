using System;
using FestiSpec.SharedCode.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SharedCode.Models
{
    public partial class FestispecContext : DbContext
    {
        public FestispecContext()
        {
        }

        public FestispecContext(DbContextOptions<FestispecContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Assignees> Assignees { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventInspection> EventInspection { get; set; }
        public virtual DbSet<EventStatus> EventStatus { get; set; }
        public virtual DbSet<FileLink> FileLink { get; set; }
        public virtual DbSet<FormQuestion> FormQuestion { get; set; }
        public virtual DbSet<InspectionForm> InspectionForm { get; set; }
        public virtual DbSet<Law> Law { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Multplechoice> Multplechoice { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; }
        public virtual DbSet<QuestionType> QuestionType { get; set; }
        public virtual DbSet<Quotation> Quotation { get; set; }
        public virtual DbSet<QuotationStatus> QuotationStatus { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<ReportHasEvents> ReportHasEvents { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserHasAvailability> UserHasAvailability { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Resources.ResourceManager.GetString("Connection String"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("answer_", "festispec");

                entity.HasIndex(e => e.FormQuestionId)
                    .HasName("fk_inspection_has_form_question_form_question1_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_answer__user1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FormQuestionId).HasColumnName("form_question_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasMaxLength(255);

                entity.HasOne(d => d.FormQuestion)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.FormQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("answer_$fk_inspection_has_form_question_form_question1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("answer_$fk_answer__user1");
            });

            modelBuilder.Entity<Assignees>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.InspectionFormId })
                    .HasName("PK_assignees_user_id");

                entity.ToTable("assignees", "festispec");

                entity.HasIndex(e => e.InspectionFormId)
                    .HasName("fk_assignees_inspection_form1_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_user_has_assignment_user1_idx");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.InspectionFormId).HasColumnName("inspection_form_id");

                entity.HasOne(d => d.InspectionForm)
                    .WithMany(p => p.Assignees)
                    .HasForeignKey(d => d.InspectionFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("assignees$fk_assignees_inspection_form1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Assignees)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("assignees$fk_user_has_assignment_user1");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer", "festispec");

                entity.HasIndex(e => e.LocationId)
                    .HasName("fk_customer_location_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(45);

                entity.Property(e => e.Kvk)
                    .IsRequired()
                    .HasColumnName("kvk")
                    .HasMaxLength(8);

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45);

                entity.Property(e => e.TelephoneNumber)
                    .HasColumnName("telephone_number")
                    .HasMaxLength(45);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customer$fk_customer_location");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("event", "festispec");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("fk_assignment_customer1_idx");

                entity.HasIndex(e => e.EventStatus)
                    .HasName("fk_assignment_assignment_status1_idx");

                entity.HasIndex(e => e.LocationId)
                    .HasName("fk_assignment_location1_idx");

                entity.HasIndex(e => e.PaymentStatus)
                    .HasName("fk_assignment_payment_status1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.About).HasColumnName("about");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.EventStatus)
                    .IsRequired()
                    .HasColumnName("event_status")
                    .HasMaxLength(15);

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45);

                entity.Property(e => e.PaymentStatus)
                    .IsRequired()
                    .HasColumnName("payment_status")
                    .HasMaxLength(15);

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("event$fk_assignment_customer1");

                entity.HasOne(d => d.EventStatusNavigation)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.EventStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("event$fk_assignment_assignment_status1");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("event$fk_assignment_location1");

                entity.HasOne(d => d.PaymentStatusNavigation)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.PaymentStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("event$fk_assignment_payment_status1");
            });

            modelBuilder.Entity<EventInspection>(entity =>
            {
                entity.ToTable("event_inspection", "festispec");

                entity.HasIndex(e => e.EventId)
                    .HasName("fk_event_has_inspection_form_event1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExecutionDate)
                    .HasColumnName("execution_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventInspection)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("event_inspection$fk_event_has_inspection_form_event1");
            });

            modelBuilder.Entity<EventStatus>(entity =>
            {
                entity.HasKey(e => e.Status)
                    .HasName("PK_event_status_status");

                entity.ToTable("event_status", "festispec");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(15)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<FileLink>(entity =>
            {
                entity.ToTable("file_link", "festispec");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnName("path")
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<FormQuestion>(entity =>
            {
                entity.ToTable("form_question", "festispec");

                entity.HasIndex(e => e.InspectionId)
                    .HasName("fk_form_question_has_inspection_idx");

                entity.HasIndex(e => e.Type)
                    .HasName("fk_form_question_type1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.InspectionId).HasColumnName("inspection_id");

                entity.Property(e => e.Instructions).HasColumnName("instructions");

                entity.Property(e => e.Order).HasColumnName("order");

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasColumnName("question")
                    .HasMaxLength(45);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(45);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Inspection)
                    .WithMany(p => p.FormQuestion)
                    .HasForeignKey(d => d.InspectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("form_question$fk_form_question_has_inspection");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.FormQuestion)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("form_question$fk_type");
            });

            modelBuilder.Entity<InspectionForm>(entity =>
            {
                entity.ToTable("inspection_form", "festispec");

                entity.HasIndex(e => e.EventInspectionId)
                    .HasName("fk_inspection_form_event_inspection1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EventInspectionId).HasColumnName("event_inspection_id");

                //entity.Property(e => e.ExecutionDate)
                //    .HasColumnName("execution_date")
                //    .HasColumnType("datetime")
                //    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FloorPlan)
                    .HasColumnName("floor_plan")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.EventInspection)
                    .WithMany(p => p.InspectionForm)
                    .HasForeignKey(d => d.EventInspectionId)
                    .HasConstraintName("inspection_form$fk_inspection_form_id");
            });

            modelBuilder.Entity<Law>(entity =>
            {
                entity.ToTable("law", "festispec");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Law)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("law$fk_location_id");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location", "festispec");

                entity.HasIndex(e => new { e.Postalcode, e.Number, e.City })
                    .HasName("unique_location")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(45);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longtitude).HasColumnName("longtitude");

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasMaxLength(45);

                entity.Property(e => e.Postalcode)
                    .HasColumnName("postalcode")
                    .HasMaxLength(6);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Multplechoice>(entity =>
            {
                entity.ToTable("multplechoice", "festispec");

                entity.HasIndex(e => e.FormQuestionId)
                    .HasName("fk_multplechoice_form_question1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FormQuestionId).HasColumnName("form_question_id");

                entity.Property(e => e.Option)
                    .IsRequired()
                    .HasColumnName("option")
                    .HasMaxLength(45);

                entity.HasOne(d => d.FormQuestion)
                    .WithMany(p => p.Multplechoice)
                    .HasForeignKey(d => d.FormQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("multplechoice$fk_multplechoice_form_question1");
            });

            modelBuilder.Entity<PaymentStatus>(entity =>
            {
                entity.HasKey(e => e.PaymentStatus1)
                    .HasName("PK_payment_status_payment_status");

                entity.ToTable("payment_status", "festispec");

                entity.Property(e => e.PaymentStatus1)
                    .HasColumnName("payment_status")
                    .HasMaxLength(15)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.HasKey(e => e.QuestionType1)
                    .HasName("PK_type_question_type");

                entity.ToTable("question_type", "festispec");

                entity.Property(e => e.QuestionType1)
                    .HasColumnName("question_type")
                    .HasMaxLength(45)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Quotation>(entity =>
            {
                entity.ToTable("quotation", "festispec");

                entity.HasIndex(e => e.EventId)
                    .HasName("fk_offerte_assignment1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.QuotationStatusStatus)
                    .IsRequired()
                    .HasColumnName("quotation status_status")
                    .HasMaxLength(45);

                entity.Property(e => e.Text).HasColumnName("text");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Quotation)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("quotation$fk_offerte_assignment1");

                entity.HasOne(d => d.QuotationStatusStatusNavigation)
                    .WithMany(p => p.Quotation)
                    .HasForeignKey(d => d.QuotationStatusStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("quotation$fk_quotation_quotation status1");
            });

            modelBuilder.Entity<QuotationStatus>(entity =>
            {
                entity.HasKey(e => e.Status)
                    .HasName("PK_quotation status_status");

                entity.ToTable("quotation status", "festispec");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(45)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("report", "festispec");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.FileLinkId).HasColumnName("file_link_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.FileLink)
                    .WithMany(p => p.Report)
                    .HasForeignKey(d => d.FileLinkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_report_file");
            });

            modelBuilder.Entity<ReportHasEvents>(entity =>
            {
                entity.HasKey(e => new { e.ReportId, e.EventId });

                entity.ToTable("report_has_events", "festispec");

                entity.Property(e => e.ReportId).HasColumnName("report_id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.ReportHasEvents)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_event_id");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportHasEvents)
                    .HasForeignKey(d => d.ReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_report_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user", "festispec");

                entity.HasIndex(e => e.LocationId)
                    .HasName("fk_user_location1_idx");

                entity.HasIndex(e => e.UserRole)
                    .HasName("fk_user_user_role1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Avatar)
                    .HasColumnName("avatar")
                    .HasColumnType("image");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(45);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(45);

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.MiddleName)
                    .HasColumnName("middle_name")
                    .HasMaxLength(45);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserRole)
                    .IsRequired()
                    .HasColumnName("user_role")
                    .HasMaxLength(10);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("user$fk_user_location1");

                entity.HasOne(d => d.UserRoleNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.UserRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user$fk_user_user_role1");
            });

            modelBuilder.Entity<UserHasAvailability>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.Day })
                    .HasName("PK_user_has_availability_user_id");

                entity.ToTable("user_has_availability", "festispec");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_user_has_availability_user1_idx");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Day)
                    .HasColumnName("day")
                    .HasColumnType("date");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserHasAvailability)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_has_availability$fk_user_has_availability_user1");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.Role)
                    .HasName("PK_user_role_role");

                entity.ToTable("user_role", "festispec");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(10)
                    .ValueGeneratedNever();
            });
        }
    }
}
