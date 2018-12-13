using System;
using System.Collections.Generic;
using System.Text;

namespace API.EventBus.Entities
{
    public class Stoc
    {
        public int ID { get; set; }
        public object Pusher { get; set; }
        public string Consumer { get; set; }
        public object Value { get; set; }
    }
}
