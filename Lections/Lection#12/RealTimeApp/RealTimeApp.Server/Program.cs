using RealTimeApp.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<ChatHub>("/chat"); // http://localhost:5082/chat

app.Run();
