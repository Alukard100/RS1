using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"New connection: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }
        // This method will be called to join a group based on the user's token (e.g., userId)
        public async Task JoinGroup(string userId)
        {
            // Add the client to a group using the userId as the group name
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            Console.WriteLine($"User {userId} joined group {userId}");
        }

        // Method to send a message to a specific group
        public async Task SendMessage(string userId, string message)
        {
            Console.WriteLine($"Sending message to {userId}: {message}");
            await Clients.Group(userId).SendAsync("ReceiveMessage", message);
        }

        // This is called when a client disconnects
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"Client {Context.ConnectionId} disconnected.");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
