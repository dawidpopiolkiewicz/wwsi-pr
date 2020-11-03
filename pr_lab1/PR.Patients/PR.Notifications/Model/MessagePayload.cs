using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PR.Notifications.Model
{
    public class MessagePayload
    {
        public string EventName { get; set; }
        public string EmailAddress { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
