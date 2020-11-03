using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using PR.Notifications.Model;

namespace PR.Patients.Services
{
    public class EmailSender
    {

        public void Send(MessagePayload payload)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("dpwwsi@gmail.com", "C2%~8SAcmW)*QsXC"),
                EnableSsl = true,
            };


            smtpClient.Send("dpwwsi@gmail.com", payload.EmailAddress, payload.Title, payload.Message);
        }
    }
}
