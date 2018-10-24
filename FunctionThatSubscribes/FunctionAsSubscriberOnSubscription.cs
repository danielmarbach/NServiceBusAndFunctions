using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace FunctionThatSubscribes
{
    public static class FunctionAsSubscriberOnSubscription
    {
        [FunctionName("FunctionAsSubscriberOnSubscription")]
        public static void Run([ServiceBusTrigger("bundle-1", "Samples.ASBS.FunctionAsSubscriberOnSubscription", AccessRights.Manage, Connection = "AzureServiceBusConnectionString")]string myEventAsString, TraceWriter log)
        {
            var myEvent = JsonConvert.DeserializeObject<MyEvent>(myEventAsString);
            log.Info($"C# ServiceBus queue trigger function processed message: {myEvent.Property}");
        }
    }
}