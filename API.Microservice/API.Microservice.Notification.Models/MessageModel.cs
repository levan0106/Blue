using System;

namespace API.Microservice.Notification.Models
{
    public class MessageModel
    {
        public int ID { get; set; }
        public string Consumer { get; set; }
        public object Value { get; set; }
    }
}
