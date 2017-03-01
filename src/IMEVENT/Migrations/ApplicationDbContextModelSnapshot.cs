using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using IMEVENT.Data;

namespace IMEVENT.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IMEVENT.Data.Dormitory", b =>
                {
                    b.Property<int>("IdDormitory")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Capacity");

                    b.Property<int>("DormType");

                    b.Property<int>("IdEvent");

                    b.Property<string>("Name");

                    b.HasKey("IdDormitory");

                    b.ToTable("Dorms");
                });

            modelBuilder.Entity("IMEVENT.Data.Event", b =>
                {
                    b.Property<int>("IdEvent")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("Fee");

                    b.Property<bool>("MingleAttendees");

                    b.Property<string>("Place");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Theme");

                    b.Property<int>("Type");

                    b.HasKey("IdEvent");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("IMEVENT.Data.EventAttendee", b =>
                {
                    b.Property<int>("IdEventAttendee")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmountPaid");

                    b.Property<int>("BedNbr");

                    b.Property<int>("DormType");

                    b.Property<int>("IdDormitory");

                    b.Property<int>("IdEvent");

                    b.Property<int>("IdHall");

                    b.Property<int>("IdRefectory");

                    b.Property<string>("InvitedBy");

                    b.Property<bool>("OnDiet");

                    b.Property<string>("Precision");

                    b.Property<string>("Regime");

                    b.Property<string>("Remarks");

                    b.Property<int>("SeatNbr");

                    b.Property<int>("SectionType");

                    b.Property<int>("TableNbr");

                    b.Property<int>("TableSeatNbr");

                    b.Property<int>("TableType");

                    b.Property<string>("UserId");

                    b.HasKey("IdEventAttendee");

                    b.ToTable("EventAttendees");
                });

            modelBuilder.Entity("IMEVENT.Data.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdResponsable");

                    b.Property<int>("IdSousZone");

                    b.Property<int>("IdZone");

                    b.Property<string>("Label");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("IMEVENT.Data.Hall", b =>
                {
                    b.Property<int>("IdHall")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Capacity");

                    b.Property<int>("HallType");

                    b.Property<int>("IdEvent");

                    b.Property<string>("Name");

                    b.HasKey("IdHall");

                    b.ToTable("Halls");
                });

            modelBuilder.Entity("IMEVENT.Data.Refectory", b =>
                {
                    b.Property<int>("IdRefectory")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Capacity");

                    b.Property<int>("IdEvent");

                    b.Property<string>("Name");

                    b.Property<int>("NumberOfTable");

                    b.Property<int>("TableCapacity");

                    b.HasKey("IdRefectory");

                    b.ToTable("Refectories");
                });

            modelBuilder.Entity("IMEVENT.Data.Responsable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdEntity");

                    b.Property<int>("IdUSer");

                    b.HasKey("Id");

                    b.ToTable("Responsables");
                });

            modelBuilder.Entity("IMEVENT.Data.SousZone", b =>
                {
                    b.Property<int>("IdSousZone")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdParent");

                    b.Property<int>("IdZone");

                    b.Property<string>("Label");

                    b.HasKey("IdSousZone");

                    b.ToTable("SousZones");
                });

            modelBuilder.Entity("IMEVENT.Data.Table", b =>
                {
                    b.Property<int>("IdTable")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Capacite");

                    b.Property<bool>("ForSpecialRegime");

                    b.Property<int>("IdRefertoire");

                    b.Property<string>("Name");

                    b.Property<int>("RegimeType");

                    b.HasKey("IdTable");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("IMEVENT.Data.User", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<int>("Category");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("DateofBirth");

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<int>("IdGroup");

                    b.Property<int>("IdSousZone");

                    b.Property<int>("IdZone");

                    b.Property<bool>("IsGroupResponsible");

                    b.Property<string>("Language");

                    b.Property<string>("LastName");

                    b.Property<int>("Level");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<int>("OriginZone");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Sex");

                    b.Property<int>("Status");

                    b.Property<string>("Town");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("IMEVENT.Data.UsersZone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("UserId");

                    b.Property<int>("ZoneId");

                    b.HasKey("Id");

                    b.ToTable("UsersZones");
                });

            modelBuilder.Entity("IMEVENT.Data.Zone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Label");

                    b.HasKey("Id");

                    b.ToTable("Zones");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("IMEVENT.Data.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("IMEVENT.Data.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IMEVENT.Data.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
