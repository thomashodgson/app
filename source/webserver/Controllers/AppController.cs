using System;
using System.Net.Http;
using System.Web.Http;
using messaging;
using models;
using models.Urls;

namespace webserver.Controllers
{
    [Authorize]
    public class AppController : ApiController
    {
        private readonly IMessageBus _messageBus;

        public AppController(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        [HttpPost, Route(RequestUrls.Hello)]
        public void Hello(MessageData data)
        {
            _messageBus.Send(Event.HelloRequested, CreateMessage(data));
        }

        [HttpPost, Route(RequestUrls.Bye)]
        public void Bye(MessageData data)
        {
            _messageBus.Send(Event.HelloRequested, CreateMessage(data));
        }

        private Message CreateMessage(MessageData data)
        {
            string requestUrl = ControllerContext.RouteData.Route.RouteTemplate;
            string userId = Request.GetOwinContext().Authentication.User.Identity.Name;
            return new Message(Guid.NewGuid(), requestUrl, models.User.FromUserId(userId), data);
        }
    }
}
