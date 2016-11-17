using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using Microsoft.ServiceBus;


namespace Demo1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Définition des variables
            //Variables pour l'event hub
            //Endpoint = sb://iotlabdemo-eventhub-umair.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=QsDAb7amI0Eu1od3oZr/ch2tncxQfYTEi5xVZpp5Gyw=

            string eventHubName = "de-eventhub-demo";
            string eventHubNamespace = "iotlabdemo-eventhub-umair";
            string sharedAccessPolicyName = "RootManageSharedAccessKey";
            string sharedAccessPolicyKey = "QsDAb7amI0Eu1od3oZr/ch2tncxQfYTEi5xVZpp5Gyw=";


            Random MyRandom = new Random();

            //Initialisation du serice bus
            var settings = new MessagingFactorySettings()
            {
                TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(sharedAccessPolicyName, sharedAccessPolicyKey),
                TransportType = TransportType.Amqp
            };

            var factory = MessagingFactory.Create(ServiceBusEnvironment.CreateServiceUri("sb", eventHubNamespace, ""), settings);
            EventHubClient client = factory.CreateEventHubClient(eventHubName);

            List<Task> tasks = new List<Task>();

            //Création d'une variable liste pour stocker mes objets 
            List<Device> MyDevices = new List<Device>();

            List<string> Liste = new List<string>();
            Liste.Add("Wing1");
            Liste.Add("Wing2");
            Liste.Add("Wing3");
            Liste.Add("Wing4");
            Liste.Add("Wing5");
            

            MyDevices.Add(new Device(1, MyRandom.Next(0, 25), MyRandom.Next(0, 600), MyRandom.Next(0, 10), MyRandom.Next(0, 35), MyRandom.Next(0, 359), MyRandom.Next(0, 359), MyRandom.Next(0, 500), MyRandom.Next(0, 2000), MyRandom.Next(0, 2000), MyRandom.Next(-180, 180), MyRandom.Next(-90, 90), "Tunis"));
            MyDevices.Add(new Device(2, MyRandom.Next(0, 25), MyRandom.Next(0, 600), MyRandom.Next(0, 10), MyRandom.Next(0, 35), MyRandom.Next(0, 359), MyRandom.Next(0, 359), MyRandom.Next(0, 500), MyRandom.Next(0, 2000), MyRandom.Next(0, 2000), MyRandom.Next(-180, 180), MyRandom.Next(-90, 90), "France"));
            MyDevices.Add(new Device(3, MyRandom.Next(0, 25), MyRandom.Next(0, 600), MyRandom.Next(0, 10), MyRandom.Next(0, 35), MyRandom.Next(0, 359), MyRandom.Next(0, 359), MyRandom.Next(0, 500), MyRandom.Next(0, 2000), MyRandom.Next(0, 2000), MyRandom.Next(-180, 180), MyRandom.Next(-90, 90), "Algerie"));
            MyDevices.Add(new Device(4, MyRandom.Next(0, 25), MyRandom.Next(0, 600), MyRandom.Next(0, 10), MyRandom.Next(0, 35), MyRandom.Next(0, 359), MyRandom.Next(0, 359), MyRandom.Next(0, 500), MyRandom.Next(0, 2000), MyRandom.Next(0, 2000), MyRandom.Next(-180, 180), MyRandom.Next(-90, 90), "Sousse"));
            MyDevices.Add(new Device(5, MyRandom.Next(0, 25), MyRandom.Next(0, 600), MyRandom.Next(0, 10), MyRandom.Next(0, 35), MyRandom.Next(0, 359), MyRandom.Next(0, 359), MyRandom.Next(0, 500), MyRandom.Next(0, 2000), MyRandom.Next(0, 2000), MyRandom.Next(-180, 180), MyRandom.Next(-90, 90), "Allemagne"));
        
            while (!Console.KeyAvailable)
            {
                for (int j = 0; j < 5; j++)
                {


                    var Device = MyDevices[j];

                    // Serialize to JSON
                    var serializedString = JsonConvert.SerializeObject(Device);
                    Console.WriteLine(serializedString);
                    EventData data = new EventData(Encoding.UTF8.GetBytes(serializedString))
                    {
                        PartitionKey = Device.DeviceID.ToString()
                    };

                    // Send the metric to Event Hub
                    tasks.Add(client.SendAsync(data));

                }
                Thread.Sleep(1500);
            }


        }
    }
}
