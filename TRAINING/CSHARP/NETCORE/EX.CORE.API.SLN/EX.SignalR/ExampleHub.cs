using EX.DTO;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace EX.SignalR
{
    public class ExampleHub : Hub
    {
        public async Task SendAll(string key, Message message)
        {
            await Clients.All.SendAsync("UpdateExample", key, message);
        }
    }
}
