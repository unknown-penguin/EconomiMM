﻿// <auto-generated />
using System;
using EconomiMM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EconomiMM.Migrations
{
    [DbContext(typeof(EconomiMMContext))]
    [Migration("20231031205306_addColorMaterialAttr")]
    partial class addColorMaterialAttr
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ColorMaterial", b =>
                {
                    b.Property<int>("ColorsId")
                        .HasColumnType("int");

                    b.Property<int>("MaterialsId")
                        .HasColumnType("int");

                    b.HasKey("ColorsId", "MaterialsId");

                    b.HasIndex("MaterialsId");

                    b.ToTable("ColorMaterial");
                });

            modelBuilder.Entity("EconomiMM.Models.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ColorHex")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

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

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("EconomiMM.Models.Product", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real");

                    b.Property<float>("flangeWidth")
                        .HasColumnType("real");

                    b.Property<int>("jointShape")
                        .HasColumnType("int");

                    b.Property<int>("jointType")
                        .HasColumnType("int");

                    b.Property<float>("koef")
                        .HasColumnType("real");

                    b.Property<float>("linerPartBindingWidth")
                        .HasColumnType("real");

                    b.Property<float>("linerPartHeight")
                        .HasColumnType("real");

                    b.Property<float>("linerPartLength")
                        .HasColumnType("real");

                    b.Property<float>("linerPartWidth")
                        .HasColumnType("real");

                    b.Property<float>("linerPartWorkPrice")
                        .HasColumnType("real");

                    b.Property<float?>("mainPartDiameter")
                        .HasColumnType("real");

                    b.Property<float?>("mainPartLength1")
                        .HasColumnType("real");

                    b.Property<float>("mainPartLength2")
                        .HasColumnType("real");

                    b.Property<float>("mainPartWidth")
                        .HasColumnType("real");

                    b.Property<float>("mainPartWorkPrice")
                        .HasColumnType("real");

                    b.Property<string>("orderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("siliconeTubes")
                        .HasColumnType("int");

                    b.Property<string>("temperature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("withLiner")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EconomiMM.Models.SelectedMaterial<EconomiMM.Models.ExpansionJointMaterial>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("ProductId");

                    b.ToTable("SelectedMaterial<ExpansionJointMaterial>");
                });

            modelBuilder.Entity("EconomiMM.Models.SelectedMaterial<EconomiMM.Models.FlangeMaterial>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("ProductId");

                    b.ToTable("SelectedMaterial<FlangeMaterial>");
                });

            modelBuilder.Entity("EconomiMM.Models.SelectedMaterial<EconomiMM.Models.LinerExpansionJointMaterial>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("ProductId");

                    b.ToTable("SelectedMaterial<LinerExpansionJointMaterial>");
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

            modelBuilder.Entity("ColorMaterial", b =>
                {
                    b.HasOne("EconomiMM.Models.Color", null)
                        .WithMany()
                        .HasForeignKey("ColorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EconomiMM.Models.Material", null)
                        .WithMany()
                        .HasForeignKey("MaterialsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("EconomiMM.Models.SelectedMaterial<EconomiMM.Models.ExpansionJointMaterial>", b =>
                {
                    b.HasOne("EconomiMM.Models.ExpansionJointMaterial", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EconomiMM.Models.Product", null)
                        .WithMany("ExpansionJointsMaterials")
                        .HasForeignKey("ProductId");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("EconomiMM.Models.SelectedMaterial<EconomiMM.Models.FlangeMaterial>", b =>
                {
                    b.HasOne("EconomiMM.Models.FlangeMaterial", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EconomiMM.Models.Product", null)
                        .WithMany("flangeMaterials")
                        .HasForeignKey("ProductId");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("EconomiMM.Models.SelectedMaterial<EconomiMM.Models.LinerExpansionJointMaterial>", b =>
                {
                    b.HasOne("EconomiMM.Models.LinerExpansionJointMaterial", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EconomiMM.Models.Product", null)
                        .WithMany("LinerExpansionJointMaterials")
                        .HasForeignKey("ProductId");

                    b.Navigation("Material");
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

            modelBuilder.Entity("EconomiMM.Models.Product", b =>
                {
                    b.Navigation("ExpansionJointsMaterials");

                    b.Navigation("LinerExpansionJointMaterials");

                    b.Navigation("flangeMaterials");
                });
#pragma warning restore 612, 618
        }
    }
}
