using HUB.SignalR.DTO;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace HUB.SignalR
{
    public class CustomerHub : Hub
    {
        public CustomerHub()
        {

        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendTo(string name, CustomerInfo message)
        {
            await SendProxyAsync(Clients.All, "NewCustomerMessage", name, message);
        }

        public async Task SendToExcept(string group, string name, CustomerInfo message)
        {
            await SendProxyAsync(Clients.AllExcept(group), "NewCustomerMessage", name, message);
        }

        public async Task SendToGroup(string group, string name, CustomerInfo message)
        {
            await SendProxyAsync(Clients.Group(group), "NewCustomerMessage", name, message);
        }

        public async Task SendToGroups(string name, CustomerInfo message, params string[] groups)
        {
            await SendProxyAsync(Clients.Groups(groups), "NewCustomerMessage", name, message);
        }

        public async Task SendToUser(string user, string name, CustomerInfo message)
        {
            await SendProxyAsync(Clients.User(user), "NewCustomerMessage", name, message);
        }

        public async Task SendToUsers(string name, CustomerInfo message, params string[] users)
        {
            await SendProxyAsync(Clients.Users(users), "NewCustomerMessage", name, message);
        }

        public async Task GetUpdate(int orderId)
        {
            CustomerInfo info;
            do
            {
                await Task.Delay(TimeSpan.FromSeconds(10d));
                info = new CustomerInfo
                {
                    CustomerID = "C1234",
                    Description = "Customer 1234",
                    FullName = "John Tellewoyan"
                };
                await Clients.Caller.SendAsync("ReceiveOrderUpdate", info);
            } while (info != null);
            await Clients.Caller.SendAsync("ReceiveOrderUpdate", "Completed Customer's Order Update!");
            await Clients.Caller.SendAsync("Finished");
        }

        private async Task SendProxyAsync(IClientProxy proxy, string fnName, string name, CustomerInfo message)
        {
            if (proxy == null)
            {
                return;
            }
            await proxy.SendAsync(fnName, name, message);
        }
    }
}
