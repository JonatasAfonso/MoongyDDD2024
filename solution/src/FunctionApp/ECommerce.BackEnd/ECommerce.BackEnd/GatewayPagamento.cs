using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ECommerce.BackEnd
{
    public class GatewayPagamento
    {
        [FunctionName("GatewayPagamento")]
        public void Run([ServiceBusTrigger("Pagamentos", Connection = "")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
