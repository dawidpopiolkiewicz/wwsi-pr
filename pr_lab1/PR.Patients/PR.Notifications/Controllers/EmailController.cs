using Microsoft.AspNetCore.Mvc;
using PR.Patients.Services;

namespace PR.Notifications.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        [HttpPost]
        public void SendMessage(Model.MessagePayload payload)
        {
            EmailSender sender = new EmailSender();
            sender.Send(payload);
        }
    }
}
