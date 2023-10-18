﻿// <auto-generated />
using EconomiMM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EconomiMM.Migrations
{
    [DbContext(typeof(EconomiMMContext))]
    partial class EconomiMMContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EconomiMM.Models.ExpansionJointMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<float>("Thickness")
                        .HasColumnType("real");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<string>("normilized_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("ExpansionJointsMaterials");
                });

            modelBuilder.Entity("EconomiMM.Models.ExpansionJointMaterialType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("JointMaterialTypes");
                });

            modelBuilder.Entity("EconomiMM.Models.FlangeMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<float>("Thickness")
                        .HasColumnType("real");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<string>("normilized_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("FlangeMaterials");
                });

            modelBuilder.Entity("EconomiMM.Models.HubConnection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConnectionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HubConnections");
                });

            modelBuilder.Entity("EconomiMM.Models.LinerExpansionJointMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PartOfLiner")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<float>("Thickness")
                        .HasColumnType("real");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<string>("normilized_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("LinerMaterials");
                });

            modelBuilder.Entity("EconomiMM.Models.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Reserved")
                        .HasColumnType("int");

                    b.Property<int>("Sold")
                        .HasColumnType("int");

                    b.Property<float>("Thickness")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Material");
                });

            modelBuilder.Entity("EconomiMM.Models.MaterialType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MaterialType");
                });

            modelBuilder.Entity("EconomiMM.Models.SellHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("Sold")
                        .HasColumnType("int");

                    b.Property<int>("materialId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("materialId");

                    b.ToTable("SellHistory");
                });

            modelBuilder.Entity("EconomiMM.Models.ExpansionJointMaterial", b =>
                {
                    b.HasOne("EconomiMM.Models.ExpansionJointMaterialType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("EconomiMM.Models.FlangeMaterial", b =>
                {
                    b.HasOne("EconomiMM.Models.ExpansionJointMaterialType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("EconomiMM.Models.LinerExpansionJointMaterial", b =>
                {
                    b.HasOne("EconomiMM.Models.ExpansionJointMaterialType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("EconomiMM.Models.SellHistory", b =>
                {
                    b.HasOne("EconomiMM.Models.Material", "material")
                        .WithMany()
                        .HasForeignKey("materialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("material");
                });
#pragma warning restore 612, 618
        }
    }
}
