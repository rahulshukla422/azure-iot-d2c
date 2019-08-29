using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace IoTDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**************** Appplication for sending messages to IoT hub********************");

            SimulatedDeviceClient simulatedDeviceClient = new SimulatedDeviceClient();
            simulatedDeviceClient.SendDeviceToCloudMessage().GetAwaiter().GetResult();

            Console.ReadLine();
        }

    }
}
