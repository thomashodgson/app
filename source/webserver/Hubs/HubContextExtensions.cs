using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using messaging;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace webserver.Hubs
{
    public static class HubContextExtensions
    {
        public static void ReturnUpdateToSender(this IHubContext context, Message message, dynamic data)
        {
            var userId = message.User.Id;
            var jsonObject = JsonConvert.SerializeObject(data);
            context.Clients.User(userId)
                .updateExistingViewModel(jsonObject);
        }
    }
}
