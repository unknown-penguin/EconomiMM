using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EconomiMM.Models;
using System.Reflection.Metadata;

namespace EconomiMM.Data
{
    public class EconomiMMContext : DbContext
    {
        public EconomiMMContext()
        {
        }

        public EconomiMMContext (DbContextOptions<EconomiMMContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SellHistory>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();
        }

        public DbSet<Material> Material { get; set; }

        public DbSet<MaterialType>? MaterialType { get; set; }

        public DbSet<SellHistory> SellHistory { get; set; }
        public DbSet<HubConnection>? HubConnections { get; set; }
        public DbSet<ExpansionJointMaterialType> JointMaterialTypes { get; set; }
        public DbSet<ExpansionJointMaterial> ExpansionJointsMaterials { get; set; }
        public DbSet<FlangeMaterial> FlangeMaterials { get; set; }

        public DbSet<LinerExpansionJointMaterial> LinerMaterials { get; set; }

    }
}
