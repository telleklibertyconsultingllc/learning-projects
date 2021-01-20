using HUB.SignalR.DTO;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace HUB.SignalR
{
    public class CustomerHubProxy : Hub
    {
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
                await Clients.Caller.SendAsync("ReceiveOrderUpdate");
            } while (info != null);
            await Clients.Caller.SendAsync("Finished");
        }
    }
}
