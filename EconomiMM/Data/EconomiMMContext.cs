using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EconomiMM.Models;
using System.Reflection.Metadata;
using Microsoft.Extensions.Hosting;

namespace EconomiMM.Data
{
    public class EconomiMMContext : DbContext
    {
        public EconomiMMContext()
        {
        }

        public EconomiMMContext(DbContextOptions<EconomiMMContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SellHistory>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Material>()
                .HasMany(e => e.Colors)
                .WithMany(e => e.Materials)
                .UsingEntity<ColorMaterial>();

            modelBuilder.Entity<ColorMaterial>()
                .HasKey(cm => new { cm.MaterialId, cm.ColorId });

            


        }



        public DbSet<Material> Material { get; set; }

        public DbSet<MaterialType>? MaterialType { get; set; }

        public DbSet<PriceKoeficients>? PriceKoeficients { get; set; }
        public DbSet<SellHistory> SellHistory { get; set; }
        public DbSet<HubConnection>? HubConnections { get; set; }
        public DbSet<ExpansionJointMaterialType> JointMaterialTypes { get; set; }
        public DbSet<ExpansionJointMaterial> ExpansionJointsMaterials { get; set; }
        public DbSet<FlangeMaterial> FlangeMaterials { get; set; }

        public DbSet<LinerExpansionJointMaterial> LinerMaterials { get; set; }
        public DbSet<Product> Orders { get; set; }
        public DbSet<Color>? Colors { get; set; }

        public DbSet<ColorMaterial> colorMaterials { get; set; }

    }
}
