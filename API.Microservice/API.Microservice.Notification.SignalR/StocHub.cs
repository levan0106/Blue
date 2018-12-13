using API.Microservice.Notification.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Microservice.Notification.SignalR
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
        public Task PushNotify(MessageModel stocData)
        {
            return Clients.Group(stocData.Consumer).SendAsync("ChangeStocValue", stocData);
        }
    }
}
