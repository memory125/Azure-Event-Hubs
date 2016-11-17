using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;


namespace ServiceBusTest
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // Service Bus and Queue are created on Azure portal
            var connectionString = "{You Service Bus Connection String}";
            var queueName = "{Your Queue Name}";
            var number = 1;
            var client = QueueClient.CreateFromConnectionString(connectionString, queueName);
                      
            while (true)
            {
                var infoMessage = string.Format("ServiceBus-Demo-De! This is the {0} message!", number);
                var message = new BrokeredMessage(infoMessage);
                client.Send(message);
                Console.WriteLine(infoMessage);
                number++;
            }
        }
    }
}
