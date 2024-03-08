using eCommerceAPI.Application.Abstractions.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.SignalR.HubsService
{
    public class OrderHubService : IOrderHubService
    {
        private readonly IHubContext _hubContext;

        public OrderHubService(IHubContext hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task OrderAddedMessageASync(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.OrderAddedMessage, message);
        }
    }
}
