using Microsoft.Azure.Management.DataFactories.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Management.DataFactories.Models;


namespace RecoDFActivity
{
    public class activity : IDotNetActivity
    {
        private string modelid = String.Empty;
        private string accountKey = String.Empty;
        private string baseUri = String.Empty;
        public IDictionary<string, string> Execute(IEnumerable<LinkedService> linkedServices, IEnumerable<Dataset> datasets, Activity activity, IActivityLogger logger)
        {
            logger.Write("Start activity");
            DotNetActivity dotNetActivityPipeline = (DotNetActivity)activity.TypeProperties;
            modelid = dotNetActivityPipeline.ExtendedProperties["modelid"];
            accountKey = dotNetActivityPipeline.ExtendedProperties["accountKey"];
            baseUri = dotNetActivityPipeline.ExtendedProperties["baseuri"];
            logger.Write("modelId: {0}; AccountKey:{1}; BaseUri: {2}", modelid, accountKey, baseUri);

            logger.Write( "Start with Export");
            try
            {
                var inputDataset = datasets.Single(dataset => dataset.Name == activity.Inputs.Single().Name);
                var ouputDataset = datasets.Single(dataset => dataset.Name == activity.Outputs.Single().Name);
                logger.Write(inputDataset.Name);

                //RecommendationsApiWrapper api = new RecommendationsApiWrapper(accountKey, baseUri);



            }
            catch (Exception ex)
            {
                logger.Write("Error occurred: {0} -  {1}", ex.Message, ex.InnerException.ToString());
            }
            logger.Write("end");
            return new Dictionary<string, string>();
        }
    }
}
