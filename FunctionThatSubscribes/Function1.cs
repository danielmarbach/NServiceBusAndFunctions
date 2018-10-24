using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;

namespace FunctionThatSubscribes
{
    public static class FunctionAsSubscriberOnQueue
    {
        [FunctionName("FunctionAsSubscriberOnQueue")]
        public static void Run([ServiceBusTrigger("samples.asbs.functionassubscriberonqueue", AccessRights.Manage, Connection = "AzureServiceBusConnectionString")]MyEvent myQueueItem, TraceWriter log)
        {
            log.Info($"C# ServiceBus queue trigger function processed message: {myQueueItem.Property}");
        }
    }
}
