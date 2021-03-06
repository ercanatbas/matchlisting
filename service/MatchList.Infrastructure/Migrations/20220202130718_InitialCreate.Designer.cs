// <auto-generated />
using System;
using MatchList.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MatchList.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220202130718_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("MatchList.Domain.Matches.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AwayTeam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("awayteam");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("country");

                    b.Property<long>("EventId")
                        .HasColumnType("bigint")
                        .HasColumnName("eventid");

                    b.Property<DateTime>("EventTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("eventtime");

                    b.Property<int>("EventType")
                        .HasColumnType("integer")
                        .HasColumnName("eventtype");

                    b.Property<string>("HomeTeam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("hometeam");

                    b.Property<string>("League")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("league");

                    b.HasKey("Id")
                        .HasName("pk_matches");

                    b.HasIndex("EventId")
                        .IsUnique()
                        .HasDatabaseName("ix_matches_eventid");

                    b.ToTable("matches");
                });

            modelBuilder.Entity("MatchList.Domain.Matches.MatchAuditLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AffectedColumns")
                        .HasColumnType("text")
                        .HasColumnName("affectedcolumns");

                    b.Property<DateTime>("AuditOn")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("auditon");

                    b.Property<long>("EventId")
                        .HasColumnType("bigint")
                        .HasColumnName("eventid");

                    b.Property<string>("NewValues")
                        .HasColumnType("text")
                        .HasColumnName("newvalues");

                    b.Property<string>("OldValues")
                        .HasColumnType("text")
                        .HasColumnName("oldvalues");

                    b.HasKey("Id")
                        .HasName("pk_matchauditlogs");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_matchauditlogs_eventid");

                    b.ToTable("matchauditlogs");
                });
#pragma warning restore 612, 618
        }
    }
}
