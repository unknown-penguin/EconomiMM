
using EconomiMM.Models;
using EconomiMM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

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
        public async Task EditMaterial(Material material)
        {
            dbContext.Material.Update(material);
            await dbContext.SaveChangesAsync();
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
