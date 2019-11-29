using System;
using System.Threading.Tasks;
using DotNetCoreSqlDb.Models;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Newtonsoft.Json;

namespace DotNetCoreSqlDb.Services
{
    public class AzureQueueStroageService : IAzureQueueStroageService
    {

        public async Task Send(Todo todo)
        {
            var connectionstrings = Environment.GetEnvironmentVariable("StorageConnectionString");

            if (string.IsNullOrEmpty(connectionstrings)) return;

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionstrings);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("todoqueue");
            queue.CreateIfNotExists();

            var json = JsonConvert.SerializeObject(todo);
            CloudQueueMessage message = new CloudQueueMessage(json);

            await queue.AddMessageAsync(message);
        }
    }
}
