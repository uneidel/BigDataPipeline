using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using Microsoft.Azure.Management.DataFactories.Models;
using Microsoft.DataFactories.Runtime;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Recommendations;

using Microsoft.Azure.Management.DataFactories.Runtime;

namespace Iot
{
   

    public class IOTHubExportActivity : Microsoft.DataFactories.Runtime.IDotNetActivity
    {  
        
        public IDictionary<string, string> Execute(IEnumerable<ResolvedTable> inputTables, 
            IEnumerable<ResolvedTable> outputTables, IDictionary<string, string> extendedProperties, IActivityLogger logger)
        {
            logger.Write(System.Diagnostics.TraceEventType.Information, "Start activity");

            modelid = extendedProperties["modelid"];
            accountKey = extendedProperties["accountKey"];
            baseUri = extendedProperties["baseuri"];
            logger.Write(System.Diagnostics.TraceEventType.Information, "modelId: {0}; AccountKey:{1}; BaseUri: {2}", modelid, accountKey, baseUri);

            logger.Write(System.Diagnostics.TraceEventType.Information, "Start with Export");
            try
            {
                ResolvedTable inputDataset = datasets.Single(dataset => dataset.Name == activity.Inputs.Single().Name);
                ResolvedTable = datasets.Single(dataset => dataset.Name == activity.Outputs.Single().Name);
                RecommendationsApiWrapper api = new RecommendationsApiWrapper(accountKey, baseUri);



            }
            catch (Exception ex)
            {
                logger.Write(System.Diagnostics.TraceEventType.Error, "Error occurred: {0} -  {1}", ex.Message, ex.InnerException.ToString());
            }
            logger.Write(System.Diagnostics.TraceEventType.Information, "end");
            return new Dictionary<string, string>();
        }
    }
}
