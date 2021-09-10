using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace csv_uploader.Hubs
{
    public interface IUploaderHub
    {
        Task BroadcastMessage(string name, string message);
    }
    public class UploaderHub : Hub<IUploaderHub>
    {
        public async Task BroadcastMessage(string name, string message)
        {
            await Clients.All.BroadcastMessage(name, message);
        }
    }
}