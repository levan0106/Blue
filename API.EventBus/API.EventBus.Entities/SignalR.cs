using System;
using System.Collections.Generic;
using System.Text;

namespace API.EventBus.Entities
{
    public class SignalR
    {
        public string HubUrl { get; set; }
        public bool UseSignalR { get; set; }
        public string EventName { get; set; }
    }
}
