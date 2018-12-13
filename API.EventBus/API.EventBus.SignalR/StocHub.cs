using System;
using System.Threading.Tasks;
using API.EventBus.Entities;
using Microsoft.AspNetCore.SignalR;

namespace API.EventBus.SignalR
{
    public class StocHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return Clients.Client(Context.ConnectionId).SendAsync("SetConnectionId", Context.ConnectionId);
        }
        public async Task<string> ConnectGroup(string stocName, string connectionID)
        {
            await Groups.AddToGroupAsync(connectionID, stocName);
            return $"{connectionID} is added {stocName}";
        }
        public Task PushNotify(Stoc stocData)
        {
            return Clients.Group(stocData.Consumer).SendAsync("ChangeStocValue", stocData);
        }
    }
}
