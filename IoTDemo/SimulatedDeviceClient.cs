using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace IoTDemo
{
    public class SimulatedDeviceClient
    {
        public DeviceClient _deviceClient;
        public readonly static string _connectionString = "";

        public SimulatedDeviceClient()
        {
            _deviceClient = DeviceClient.CreateFromConnectionString(_connectionString, TransportType.Mqtt);
        }
        public async Task SendDeviceToCloudMessage()
        {
            double minTemperature = 20;
            double minHumidity = 60;

            Random rand = new Random();

            while (true)
            {
                double currentTemprature = minTemperature + rand.NextDouble() * 15;
                double currentHumidity = minHumidity + rand.NextDouble() * 20;


                var telemetoryJson = new
                {
                    temperature = currentTemprature,
                    humidity = currentHumidity
                };

                var messageString = Newtonsoft.Json.JsonConvert.SerializeObject(telemetoryJson);

                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                message.Properties.Add("temperationAlert", (currentTemprature > 30) ? "true" : "false");

                await _deviceClient.SendEventAsync(message);

                Console.WriteLine($"{DateTime.Now} Sending message : {messageString}");

                await Task.Delay(3000);
            }

        }

    }
}
