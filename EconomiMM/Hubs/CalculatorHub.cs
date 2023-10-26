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

            foreach (var materialId in selectedMaterials)
            {
                var material = dbContext.ExpansionJointsMaterials.Include(t => t.Type).Where(m => m.Id == Int32.Parse(materialId)).First();
                materials.Add(material);

            }
            await Clients.Client(connectionId).SendAsync("addSelectedMaterialJoint", materials);
        }

        public async Task SelectMaterialsFlange(List<string> selectedMaterials)
        {
            var connectionId = Context.ConnectionId;


            List<FlangeMaterial> materials = new List<FlangeMaterial>();

            foreach (var materialId in selectedMaterials)
            {
                var material = dbContext.FlangeMaterials.Include(t => t.Type).Where(m => m.Id == Int32.Parse(materialId)).First();
                materials.Add(material);

            }
            await Clients.Client(connectionId).SendAsync("addSelectedMaterialFlange", materials);
        }

        public async Task SelectMaterialsLiner(List<string> selectedMaterials)
        {
            var connectionId = Context.ConnectionId;


            List<LinerExpansionJointMaterial> materials = new List<LinerExpansionJointMaterial>();

            foreach (var materialId in selectedMaterials)
            {
                var material = dbContext.LinerMaterials.Include(t => t.Type).Where(m => m.Id == Int32.Parse(materialId)).First();
                materials.Add(material);

            }
            await Clients.Client(connectionId).SendAsync("addSelectedMaterialLiner", materials);
        }


        //private void formWordFile(string filename)
        //{

        //}


        private float calc(Product product)
        {
            float s1 = 0, sflange = 0;
            float sLiner = 0, vLiner = 0;
            if (product.jointShape == Enum.JointShapes.Circle)
            {
                if (product.jointType == Enum.JointType.Flange)
                    s1 = (float)(product.mainPartDiameter * 3.14f * (product.mainPartWidth + (2.0f * product.flangeWidth)));
                else if (product.jointType == Enum.JointType.Tape)
                    s1 = (float)(product.mainPartDiameter * 3.14f * product.mainPartWidth);

                sflange = (float)(product.mainPartDiameter * 3.14f * 2 * 2 * product.flangeWidth);
            }
            else if (product.jointShape == Enum.JointShapes.Rectangle)
            {
                if (product.jointType == Enum.JointType.Flange)
                    s1 = (float)(product.mainPartLength1 + product.mainPartLength2) * 2 * (product.mainPartWidth + 2 * product.flangeWidth);
                else if (product.jointType == Enum.JointType.Tape)
                    s1 = (float)((product.mainPartLength1 + product.mainPartLength2) * 2 * product.mainPartWidth);

                sflange = (float)((product.mainPartLength1 + 2 * product.flangeWidth) + product.mainPartLength2) * 2 * 2 * product.flangeWidth;
            }
            if (product.withLiner)
            {
                sLiner = (float)((product.mainPartLength1 * 2 + product.linerPartHeight * 2 + product.linerPartBindingWidth * 4) * product.linerPartWidth);
                vLiner = (float)(product.mainPartLength1 * product.linerPartHeight * product.linerPartWidth);
            }


            foreach (var material in product.ExpansionJointsMaterials)
            {
                for (int i = 1; i <= material.Count; i++)
                    product.TotalPrice += material.Material.Price * s1 * product.koef;
            }
            foreach (var material in product.flangeMaterials)
            {
                for (int i = 1; i <= material.Count; i++)
                    product.TotalPrice += material.Material.Price * sflange;
            }
            foreach (var material in product.LinerExpansionJointMaterials)
            {
                if (material.Material.PartOfLiner == Enum.LinerParts.Inner)
                {
                    for (int i = 1; i <= material.Count; i++)
                        product.TotalPrice += material.Material.Price * sLiner;
                }
                else if (material.Material.PartOfLiner == Enum.LinerParts.Outer)
                {
                    for (int i = 1; i <= material.Count; i++)
                        product.TotalPrice += material.Material.Price * vLiner;
                }
            }

            product.TotalPrice += product.mainPartWorkPrice;
            product.TotalPrice += product.siliconeTubes * 130;
            return product.TotalPrice;
        }
        public async Task CalculatePrice(Product orderProduct,
            List<MaterialCountTuple> jointUsedMaterials,
            List<MaterialCountTuple> flangeUsedMaterials,
            List<MaterialCountTuple> linerUsedMaterials)
        {
            if (jointUsedMaterials != null)
            {
                foreach (var material in jointUsedMaterials)
                {
                    var selectedMaterialInstance = dbContext.ExpansionJointsMaterials.Include(sh => sh.Type).Where(m => m.Id == material.id).FirstOrDefault();
                    var selectedMaterial = new SelectedMaterial<ExpansionJointMaterial>(selectedMaterialInstance, material.countOfLayers);
                    orderProduct.ExpansionJointsMaterials.Add(selectedMaterial);
                }
            }
            if (flangeUsedMaterials != null)
            {
                foreach (var material in flangeUsedMaterials)
                {
                    var selectedMaterialInstance = dbContext.FlangeMaterials.Include(sh => sh.Type).Where(m => m.Id == material.id).FirstOrDefault();
                    var selectedMaterial = new SelectedMaterial<FlangeMaterial>(selectedMaterialInstance, material.countOfLayers);
                    orderProduct.flangeMaterials.Add(selectedMaterial);
                }
            }
            if (linerUsedMaterials != null)
            {
                foreach (var material in linerUsedMaterials)
                {

                    var selectedMaterialInstance = dbContext.LinerMaterials.Include(sh => sh.Type).Where(m => m.Id == material.id).FirstOrDefault();
                    var selectedMaterial = new SelectedMaterial<LinerExpansionJointMaterial>(selectedMaterialInstance, material.countOfLayers);
                    orderProduct.LinerExpansionJointMaterials.Add(selectedMaterial);
                }
            }


            var connectionId = Context.ConnectionId;
            await Clients.Client(connectionId).SendAsync("ShowResult", calc(orderProduct));
        }

        public async Task SaveOrder(Product orderProduct,
            List<MaterialCountTuple> jointUsedMaterials,
            List<MaterialCountTuple> flangeUsedMaterials,
            List<MaterialCountTuple> linerUsedMaterials)
        {
            
            await CalculatePrice(orderProduct, jointUsedMaterials, flangeUsedMaterials, linerUsedMaterials);
            dbContext.Orders.Add(orderProduct);
            await dbContext.SaveChangesAsync();
            //add notification on saved/error
        }
    }
}
