using System.Diagnostics;
using System.Threading.Tasks;
using Autofac.Builder;
using models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace webserver.Hubs
{
    [Authorize]
    public class AppHub : Hub
    {
        public override Task OnConnected()
        {
            var userId = Context.User.Identity.GetUserId();
            var user = User.FromUserId(userId);

            Clients.Caller.updateExistingViewModel(JsonConvert.SerializeObject(
                new
                {
                    UserAuthenticationFinished = true,
                    Version = CurrentVersion(),
                    User = user,
                    Page = "Home"
                }));

            return base.OnConnected();
        }

        private static string CurrentVersion()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.FileVersion;
        }
    }
}