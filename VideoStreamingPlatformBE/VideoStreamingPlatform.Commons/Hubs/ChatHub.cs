using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(int senderId, int receiverId, string message)
        {
            await Clients.User(receiverId.ToString())
                .SendAsync("ReceiveMessage", senderId, message, DateTime.UtcNow);
        }
    }
}
