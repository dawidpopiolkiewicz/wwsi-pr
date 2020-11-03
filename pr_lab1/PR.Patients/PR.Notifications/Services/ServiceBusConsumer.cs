using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PR.Notifications.Model;
using PR.Patients.Services;

namespace PR.Notifications.Services
{
    public class ServiceBusConsumer
    {

        private readonly QueueClient _queueClient;
        private readonly string QueueName = "messages";

        public ServiceBusConsumer(IConfiguration configuration)
        {
            _queueClient = new QueueClient(configuration.GetConnectionString("ServiceBusConnectionString"), QueueName);
        }

        public void Register()
        {
            var options = new MessageHandlerOptions((e) => Task.CompletedTask)
            {
                AutoComplete = false
            };

            _queueClient.RegisterMessageHandler(ProcesssMessage, options);
        }

        public async Task SendMessage(MessagePayload payload)
        {
            string data = JsonConvert.SerializeObject(payload);
            Message message = new Message(Encoding.UTF8.GetBytes(data));
            await _queueClient.SendAsync(message);
        }

        private async Task ProcesssMessage(Message message, CancellationToken token)
        {

            var payload = JsonConvert.DeserializeObject<MessagePayload>(Encoding.UTF8.GetString(message.Body));

            if (payload.EventName == "NewPatientRegistered")
            {
                var sender = new EmailSender();
                sender.Send(payload);
            }

            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }
    }
}
