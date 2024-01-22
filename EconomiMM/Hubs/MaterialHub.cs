
using EconomiMM.Models;
using EconomiMM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace EconomiMM.Hubs
{
    public class MaterialHub : Hub
    {
        private readonly EconomiMMContext dbContext;

        public MaterialHub(EconomiMMContext dbContext)
        {
            this.dbContext = dbContext;
        } 

        public async Task SendMaterial(string name)
        {
            var materials = await dbContext.Material
                .Where(m => m.Name == name)
                .AsNoTracking()
                .Include(mat => mat.Colors)
                .ToListAsync();
            var connectionId = Context.ConnectionId;
            await Clients.Client(connectionId).SendAsync("RecievedMaterialInfo", materials);
        }

        public async Task GetAllMaterialTypes()
        {


            List<MaterialType> materialType = await dbContext.MaterialType.ToListAsync();
            var connectionId = Context.ConnectionId;
            await Clients.Client(connectionId).SendAsync("RecievedMaterialTypeInfo", materialType);
        }
        public async Task EditMaterial(Dictionary<string, string> materialObj)
        {
            var test = int.Parse(materialObj["id"]);
            var material = dbContext.Material.FirstOrDefault(m => m.Id == int.Parse(materialObj["id"]));
            material.Count = int.Parse(materialObj["count"]);
            material.Price = int.Parse(materialObj["price"]);
            material.OurPriceForSheet = int.Parse(materialObj["OurPriceForSheet"]);
            material.OurPriceForSqMetre = int.Parse(materialObj["OurPriceForSqMeter"]);
            material.DealerPriceForSheet = int.Parse(materialObj["DealerPriceForSheet"]);
            material.DealerPriceForSqMetre = int.Parse(materialObj["DealerPriceForSqMeter"]);
            material.Reserved = int.Parse(materialObj["reserved"]);
            await dbContext.SaveChangesAsync();
        }

        public async Task GetNewPrices(string manufacturer, int id, string priceString)
        {

            var price = float.Parse(priceString);
            var priceKoeficients = await dbContext.PriceKoeficients.Where(pk => pk.Manufacturer == manufacturer).FirstOrDefaultAsync();
            var material = await dbContext.Material.Where(m => m.Id == id).FirstOrDefaultAsync();
            var OurPriceForSheet = (int)CalcNewPrice(price, priceKoeficients.OurPriceForSheetKoef);
            var OurPriceForSqMetre = (int)CalcNewPriceBySize(price, priceKoeficients.OurPriceForSqMetreKoef, GetSizesOfMaterial(material.Size));
            var DealerPriceForSheet = (int)CalcNewPrice(price, priceKoeficients.DealerPriceForSheetKoef);
            var DealerPriceForSqMetre = (int)CalcNewPriceBySize(price, priceKoeficients.DealerPriceForSqMetreKoef, GetSizesOfMaterial(material.Size));
            var connectionId = Context.ConnectionId;
            await Clients.Client(connectionId).SendAsync("UpdateMaterialPrices",
                                                            id,
                                                            OurPriceForSheet,
                                                            OurPriceForSqMetre,
                                                            DealerPriceForSheet,
                                                            DealerPriceForSqMetre);
        }
        private (float, float) GetSizesOfMaterial(string materialSizeString)
        {
            if (string.IsNullOrWhiteSpace(materialSizeString))
            {
                return (1000, 1000);
            }

            string pattern = @"\b(\d{3,4})х(\d{3,4})\b";
            Regex regex = new Regex(pattern);

            Match match = regex.Match(materialSizeString);

            if (match.Success)
            {
                float width = float.Parse(match.Groups[1].Value)/1000;
                float height = float.Parse(match.Groups[2].Value)/1000;
                return (width, height);
            }
            else
            {
                return (1000, 1000);
            }

        }
        private float CalcNewPrice(float prevPrice, float koef)
        {
            if (prevPrice <= 0)
            {
                return 0;
            }
            float newPrice = prevPrice * koef;
            return (float)Math.Round(newPrice / 100.0) * 100;
        }

        private float CalcNewPriceBySize(float prevPrice, float koef, (float width, float height) size)
        {
            if (prevPrice <= 0)
            {
                return 0;
            }
            float newPrice = prevPrice / (size.height * size.width) * koef;
            return (float)Math.Round(newPrice / 100.0) * 100;
        }

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("OnConnected");
            return base.OnConnectedAsync();
        }

        public async Task SaveUserConnection(string username)
        {
            var connectionId = Context.ConnectionId;
            HubConnection hubConnection = new HubConnection
            {
                ConnectionId = connectionId,
                Username = username
            };

            dbContext.HubConnections.Add(hubConnection);
            await dbContext.SaveChangesAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var hubConnection = dbContext.HubConnections.FirstOrDefault(con => con.ConnectionId == Context.ConnectionId);
            if (hubConnection != null)
            {
                dbContext.HubConnections.Remove(hubConnection);
                dbContext.SaveChangesAsync();
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
