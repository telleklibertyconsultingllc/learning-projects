using HUB.SignalR.DTO;
using System.Threading.Tasks;

namespace HUB.SignalR.Interfaces
{
    public interface ICustomerHub
    {
        Task SendToGroup(string group, string name, CustomerInfo message);
        Task SendTo(string name, CustomerInfo message);
        Task SendToExcept(string group, string name, CustomerInfo message);
        Task SendToGroups(string name, CustomerInfo message, params string[] groups);
        Task SendToUser(string user, string name, CustomerInfo message);
        Task SendToUsers(string name, CustomerInfo message, params string[] users);

        /// Groups Connection Hub
        Task JoinGroup(string groupName);
        Task LeaveGroup(string groupName);
    }
}
