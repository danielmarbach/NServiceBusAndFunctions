using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace FunctionThatSubscribes
{
    public static class FunctionAsSubscriberOnQueue
    {
        [FunctionName("FunctionAsSubscriberOnQueue")]
        public static void Run([ServiceBusTrigger("samples.asbs.functionassubscriberonqueue", AccessRights.Manage, Connection = "AzureServiceBusConnectionString")]string myEventAsString, TraceWriter log)
        {
            var myEvent = JsonConvert.DeserializeObject<MyEvent>(myEventAsString);
            log.Info($"C# ServiceBus queue trigger function processed message: {myEvent.Property}");
        }
    }
}
