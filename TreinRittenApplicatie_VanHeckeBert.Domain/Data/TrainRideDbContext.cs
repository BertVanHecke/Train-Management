using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;

namespace TreinRittenApplicatie_VanHeckeBert.Domain.Data
{
    public partial class TrainRideDbContext : DbContext
    {
        public TrainRideDbContext()
        {
        }

        public TrainRideDbContext(DbContextOptions<TrainRideDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<BookingTicket> BookingTickets { get; set; } = null!;
        public virtual DbSet<Ride> Rides { get; set; } = null!;
        public virtual DbSet<Seat> Seats { get; set; } = null!;
        public virtual DbSet<Station> Stations { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<Train> Trains { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.\\SQL19_VIVES;Initial Catalog=TreinRittenDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasDefaultValueSql("(N'')");

                entity.Property(e => e.LastName).HasDefaultValueSql("(N'')");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AspNetUserId).HasMaxLength(450);

                entity.HasOne(d => d.AspNetUser)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.AspNetUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bookings_AspNetUsers");
            });

            modelBuilder.Entity<BookingTicket>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.BookingTickets)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingTickets_Bookings");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.BookingTickets)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingTickets_Tickets");
            });

            modelBuilder.Entity<Ride>(entity =>
            {
                entity.HasIndex(e => e.FromStationId, "IX_Rides_FromStationId");

                entity.HasIndex(e => e.ToStationId, "IX_Rides_ToStationId");

                entity.HasIndex(e => e.TrainId, "IX_Rides_TrainId");

                entity.HasOne(d => d.FromStation)
                    .WithMany(p => p.RideFromStations)
                    .HasForeignKey(d => d.FromStationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rides_FromStations");

                entity.HasOne(d => d.ToStation)
                    .WithMany(p => p.RideToStations)
                    .HasForeignKey(d => d.ToStationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rides_ToStations");

                entity.HasOne(d => d.Train)
                    .WithMany(p => p.Rides)
                    .HasForeignKey(d => d.TrainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rides_Trains");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.BookingTicket)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.BookingTicketId)
                    .HasConstraintName("FK_Seats_BookingTickets");

                entity.HasOne(d => d.Ride)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.RideId)
                    .HasConstraintName("FK_Seats_Rides");
            });

            modelBuilder.Entity<Station>(entity =>
            {
                entity.HasIndex(e => e.City, "IX_Stations_City")
                    .IsUnique();
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.PriceBusiness).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PriceEconomic).HasColumnType("decimal(18, 2)");

                entity.HasMany(d => d.Rides)
                    .WithMany(p => p.Tickets)
                    .UsingEntity<Dictionary<string, object>>(
                        "TicketRide",
                        l => l.HasOne<Ride>().WithMany().HasForeignKey("RideId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TicketRides_Rides"),
                        r => r.HasOne<Ticket>().WithMany().HasForeignKey("TicketId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TicketRides_Tickets"),
                        j =>
                        {
                            j.HasKey("TicketId", "RideId");

                            j.ToTable("TicketRides");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
