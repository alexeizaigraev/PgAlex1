using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PgAlex1
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Logi> Logis { get; set; } = null!;
        public virtual DbSet<Otbor> Otbors { get; set; } = null!;
        public virtual DbSet<Terminal> Terminals { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Department1)
                    .HasName("departments_pkey");

                entity.ToTable("departments");

                entity.Property(e => e.Department1)
                    .HasColumnName("department")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.CityType)
                    .HasColumnName("city_type")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.DistrictCity)
                    .HasColumnName("district_city")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.DistrictRegion)
                    .HasColumnName("district_region")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Edrpou)
                    .HasColumnName("edrpou")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Hous)
                    .HasColumnName("hous")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.IdTerminal)
                    .HasColumnName("id_terminal")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Koatu)
                    .HasColumnName("koatu")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Koatu2)
                    .HasColumnName("koatu2")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Partner)
                    .HasColumnName("partner")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.PartnerName)
                    .HasColumnName("partner_name")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.PostIndex)
                    .HasColumnName("post_index")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Region)
                    .HasColumnName("region")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Register)
                    .HasColumnName("register")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Street)
                    .HasColumnName("street")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.StreetType)
                    .HasColumnName("street_type")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.TaxId)
                    .HasColumnName("tax_id")
                    .HasDefaultValueSql("''::text");
            });

            modelBuilder.Entity<Logi>(entity =>
            {
                entity.ToTable("logi");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Datalog)
                    .HasColumnName("datalog")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Department)
                    .HasColumnName("department")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Kind)
                    .HasColumnName("kind")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.SerialNumber)
                    .HasColumnName("serial_number")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Termial)
                    .HasColumnName("termial")
                    .HasDefaultValueSql("''::text");
            });

            modelBuilder.Entity<Otbor>(entity =>
            {
                entity.HasKey(e => e.Term)
                    .HasName("otbor_pkey");

                entity.ToTable("otbor");

                entity.Property(e => e.Term)
                    .HasColumnName("term")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Dep)
                    .HasColumnName("dep")
                    .HasDefaultValueSql("''::text");
            });

            modelBuilder.Entity<Terminal>(entity =>
            {
                entity.HasKey(e => e.Termial)
                    .HasName("terminals_pkey");

                entity.ToTable("terminals");

                entity.Property(e => e.Termial).HasColumnName("termial");

                entity.Property(e => e.BooksArhiv)
                    .HasColumnName("books_arhiv")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.DateManufacture)
                    .HasColumnName("date_manufacture")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Department).HasColumnName("department");

                entity.Property(e => e.Finish)
                    .HasColumnName("finish")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.FiscalNumber)
                    .HasColumnName("fiscal_number")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Model)
                    .HasColumnName("model")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.OroNumber)
                    .HasColumnName("oro_number")
                    .HasDefaultValueSql("'1'::text");

                entity.Property(e => e.OroSerial)
                    .HasColumnName("oro_serial")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.OwnerRro)
                    .HasColumnName("owner_rro")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Producer)
                    .HasColumnName("producer")
                    .HasDefaultValueSql("'ДАТЕКС ООД'::text");

                entity.Property(e => e.Register)
                    .HasColumnName("register")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.RneRro)
                    .HasColumnName("rne_rro")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Sealing)
                    .HasColumnName("sealing")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Sending)
                    .HasColumnName("sending")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.SerialNumber)
                    .HasColumnName("serial_number")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Soft)
                    .HasColumnName("soft")
                    .HasDefaultValueSql("'4.00 UA'::text");

                entity.Property(e => e.Ticket1sheet)
                    .HasColumnName("ticket_1sheet")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.TicketNumber)
                    .HasColumnName("ticket_number")
                    .HasDefaultValueSql("'1'::text");

                entity.Property(e => e.TicketSerial)
                    .HasColumnName("ticket_serial")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.TicketsArhiv)
                    .HasColumnName("tickets_arhiv")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.ToRro)
                    .HasColumnName("to_rro")
                    .HasDefaultValueSql("'ТОВ ПОС'::text");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
