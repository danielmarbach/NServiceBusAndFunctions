using System;
using System.Threading.Tasks;
using NServiceBus;

class Program
{
    static async Task Main()
    {
        Console.Title = "Samples.ASBS.Publisher";

        #region config

        var endpointConfiguration = new EndpointConfiguration("Samples.ASBS.Publisher");
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.EnableInstallers();

        var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();

        var connectionString = Environment.GetEnvironmentVariable("AzureServiceBus_ConnectionString");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception("Could not read the 'AzureServiceBus_ConnectionString' environment variable. Check the sample prerequisites.");
        }
        transport.ConnectionString(connectionString);

        #endregion

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        Console.WriteLine("Press 'enter' to publish an event");
        Console.WriteLine("Press any other key to exit");

        while (true)
        {
            var key = Console.ReadKey();
            Console.WriteLine();

            if (key.Key != ConsoleKey.Enter)
            {
                break;
            }

            var message = new MyEvent
            {
                Property = "Event from Endpoint1"
            };
            await endpointInstance.Publish(message)
                .ConfigureAwait(false);
            Console.WriteLine("MyEvent sent");
        }
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}