using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventHubs;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Frontendui.Controllers
{
    [Route("api/[controller]")]
    public class InteractionController : Controller
    {
        private const string EhConnectionString = "Endpoint=sb://uneidel.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=k3dzwvx7tplXFYAtLBBxn0Lm8rldtWlXbuiApAoGGek=";
        private const string EhEntityPath = "sample";


        // POST api/values
        [HttpPost]
        public async void Post([FromBody]string value)
        {
            if (value != null)
            {
                var connectionStringBuilder = new EventHubsConnectionStringBuilder(EhConnectionString) { EntityPath = EhEntityPath };
                EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
                await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(value)));
            }
        }
    }



}
