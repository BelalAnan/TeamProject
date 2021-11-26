﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeamsIntegration.Data;

namespace TeamsIntegration.Data.Migrations
{
    [DbContext(typeof(TeamDBContext))]
    partial class TeamDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TeamsIntegration.Data.models.TeamAppInstallation", b =>
                {
                    b.Property<Guid>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AppId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsInstalled")
                        .HasColumnType("bit");

                    b.HasKey("TeamId", "AppId");

                    b.ToTable("TeamAppsInstallation");
                });

            modelBuilder.Entity("TeamsIntegration.Data.models.UnMappedClass", b =>
                {
                    b.Property<Guid>("ExternalClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastCheckedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tenants")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExternalClassId");

                    b.ToTable("UnMappedClasses");
                });
#pragma warning restore 612, 618
        }
    }
}