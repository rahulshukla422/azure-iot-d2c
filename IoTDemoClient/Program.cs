using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDemoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            EventHubClient eventHubClient = new EventHubClient();
            eventHubClient.RecieveEvents().GetAwaiter().GetResult();
        }
    }
}
