using System;
using System.Collections.Generic;
using messaging;
using models;

namespace webserver.UiUpdates
{
    interface IUiUpdatorMessageConsumerConfig
    {
        Event Event { get; }
        IEnumerable<string> RequestUrls { get; }
        Func<MessageData, dynamic> ViewModelFragmentFactory { get; }
    }
}
