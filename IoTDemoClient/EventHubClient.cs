using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
namespace IoTDemoClient
{
    public class EventHubClient
    {
        private string _eventHubConnectionString = "";
        private string _eventHubName = "";
        private string _storageAccount = "";
        private string _storageContainerName = "";
        private string _storageConnectionString = "";
        public async Task RecieveEvents()
        {
            var processor = new EventProcessorHost
                (
                    _eventHubName,
                    PartitionReceiver.DefaultConsumerGroupName,
                    _eventHubConnectionString,
                    _storageConnectionString,
                    _storageContainerName
                );

            await processor.RegisterEventProcessorAsync<SimpleEventProcessor>();

            Console.WriteLine("Recieving . Press ENTER to stop the event.");
            Console.ReadLine();

            await processor.UnregisterEventProcessorAsync();
        }
    }

    public class SimpleEventProcessor : IEventProcessor
    {
        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine($"Processor Shutting Down. Partition '{context.PartitionId}', Reason: '{reason}'.");
            return Task.CompletedTask;
        }

        public Task OpenAsync(PartitionContext context)
        {
            Console.WriteLine($"SimpleEventProcessor initialized. Partition: '{context.PartitionId}'");
            return Task.CompletedTask;
        }

        public Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            Console.WriteLine($"Error on Partition: {context.PartitionId}, Error: {error.Message}");
            return Task.CompletedTask;
        }

        public Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            foreach (var item in messages)
            {
                var data = Encoding.UTF8.GetString(item.Body.Array, item.Body.Offset, item.Body.Count);
                Console.WriteLine($"Message received- Partition: '{context.PartitionId}', Data: '{data}'");
            }

            return context.CheckpointAsync();
        }
    }
}
