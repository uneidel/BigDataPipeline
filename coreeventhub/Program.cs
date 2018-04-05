using Microsoft.Azure.EventHubs;
using System;

namespace coreeventhub
{
    class Program
    {
        
        private const string EhConnectionString = "Endpoint=sb://uneidel.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=k3dzwvx7tplXFYAtLBBxn0Lm8rldtWlXbuiApAoGGek=";
        private const string EhEntityPath = "sample";

        static void Main(string[] args)
        {
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(EhConnectionString)
            {
                EntityPath = EhEntityPath
            };
            EventHubClient eventHubClient;
            eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
            eventHubClient.SendAsync
            Console.WriteLine("Hello World!");
        }
    }
}