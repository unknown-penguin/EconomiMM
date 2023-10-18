using EconomiMM.Models;
using EconomiMM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace EconomiMM.Hubs
{
    public class CalculatorHub : Hub
    {
        private readonly EconomiMMContext dbContext;

        public CalculatorHub(EconomiMMContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SelectMaterialsJoint(List<string> selectedMaterials)
        {
            var connectionId = Context.ConnectionId;
                       
            
            List<ExpansionJointMaterial> materials = new List<ExpansionJointMaterial>();

            foreach (var materialName in selectedMaterials)
            {
                var material = dbContext.ExpansionJointsMaterials.Include(t => t.Type).Where(m => m.normilized_name == materialName).First();
                materials.Add(material);

            }
            await Clients.Client(connectionId).SendAsync("addSelectedMaterialJoint", materials);
        }

        public async Task SelectMaterialsFlange(List<string> selectedMaterials)
        {
            var connectionId = Context.ConnectionId;


            List<FlangeMaterial> materials = new List<FlangeMaterial>();

            foreach (var materialName in selectedMaterials)
            {
                var material = dbContext.FlangeMaterials.Include(t => t.Type).Where(m => m.normilized_name == materialName).First();
                materials.Add(material);

            }
            await Clients.Client(connectionId).SendAsync("addSelectedMaterialFlange", materials);
        }

        public async Task SelectMaterialsLiner(List<string> selectedMaterials)
        {
            var connectionId = Context.ConnectionId;


            List<LinerExpansionJointMaterial> materials = new List<LinerExpansionJointMaterial>();

            foreach (var materialName in selectedMaterials)
            {
                var material = dbContext.LinerMaterials.Include(t => t.Type).Where(m => m.normilized_name == materialName).First();
                materials.Add(material);

            }
            await Clients.Client(connectionId).SendAsync("addSelectedMaterialLiner", materials);
        }

        //public Task CalculateMaterial()
        //{

        //}
    }
}
