using Microsoft.AspNetCore.SignalR;

namespace RealTimeApp.Server;

public class ChatHub : Hub
{
    public async Task Send(string username, string message)
    {
        await Clients.All.SendAsync("Receive", username, message);
    }
}
