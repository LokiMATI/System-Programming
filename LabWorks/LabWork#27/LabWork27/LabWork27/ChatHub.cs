using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace LabWork27;

public class ChatHub : Hub
{
    private static ConcurrentDictionary<string, string> ConnectionToRoom = new();
    private static ConcurrentDictionary<string, string> ConnectionToUser = new();
    public async Task Send(string message)
    {
        var connectionId = Context.ConnectionId;
        if (ConnectionToRoom.TryGetValue(connectionId, out var room) &&
            ConnectionToUser.TryGetValue(connectionId, out var user))
        {
            await Clients.Group(room).SendAsync("ReceiveMessage", user, message);
        }

    }

    public async Task JoinRoom(string roomName, string userName)
    {
        var connectionId = Context.ConnectionId;

        ConnectionToRoom[connectionId] = roomName;
        ConnectionToUser[connectionId] = userName;

        await Groups.AddToGroupAsync(connectionId, roomName);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connectionId = Context.ConnectionId;
        if (ConnectionToRoom.TryGetValue(connectionId, out var room))
        {
            await Groups.RemoveFromGroupAsync(connectionId, room);
        }
    }
}
