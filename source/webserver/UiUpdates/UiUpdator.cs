using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Newtonsoft.Json;
using webserver.Hubs;

namespace webserver.UiUpdates
{
    class UiUpdator
    {
        private readonly IHubContext _hubContext;

        public UiUpdator(IConnectionManager connectionManager)
        {
            _hubContext = connectionManager.GetHubContext<AppHub>();
        }

        public void SendViewModelFragmentToUser(string userId, dynamic viewModelFragment)
        {
            var jsonObject = JsonConvert.SerializeObject(viewModelFragment);
            _hubContext.Clients.User(userId)
                .updateExistingViewModel(jsonObject);
        }
    }
}
