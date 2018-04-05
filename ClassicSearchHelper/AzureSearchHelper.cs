using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClassicSearchHelper
{
    public class AzureSearchHelper
    {
        internal string apiKey = "063E29A0014A006EFE246EAD1";
        internal string searchServiceName = "uneidel";
        internal string indexName = "simpleclub";
        internal string dataSourceName = "simpleclubdatasource";
        internal string indexerName = "simpleclubindexer";
        ISearchServiceClient searchClient = null;

        public AzureSearchHelper()
        {
            searchClient = new SearchServiceClient(searchServiceName, new SearchCredentials(apiKey));
        }
        public  void CreateClient(Action<string> logger)
        {

            DeleteExistingResources();
            logger("Resources Deleted");

            var dtsource = CreateDataSource();
            logger("DataSource created");
            CreateIndex();
            logger("Index created");
            //CreateIndexer(dtsource);
            RESTIndexer(dtsource);
            logger("indexer created");
        }

        public dynamic  SimpleSearch(string searchString)
        {
            SearchParameters sp = new SearchParameters() { SearchMode = SearchMode.All };
            var _indexClient = searchClient.Indexes.GetClient(indexName);
            var result = _indexClient.Documents.Search(searchString, sp);
            return result;
        }

        public string GetStatus()
        {
            var status = searchClient.Indexers.GetStatus(this.indexerName);
            return status.Status.ToString();
        }
        public string GetStatus(string indexerName)
        {
            var status = searchClient.Indexers.GetStatus(indexerName);
            return status.Status.ToString();
        }
        private void RESTIndexer(DataSource dataSource)
        {
            RestSharp.RestClient c = new RestSharp.RestClient($"https://{searchServiceName}.search.windows.net/");
            RestSharp.RestRequest r = new RestSharp.RestRequest($"indexers?api-version=2015-02-28-Preview", RestSharp.Method.POST);
            r.AddHeader("api-key", apiKey);
            r.AddHeader("Content-Type", "application/json");
            r.RequestFormat = DataFormat.Json;
            IndexerObject indexer = new IndexerObject()
            {
                dataSourceName = dataSource.Name,
                name = indexerName,
                targetIndexName = indexName,
                parameters = new Parameters() { configuration = new Configuration() { parsingMode = "jsonArray" } },
                schedule = new Schedule() { interval = "PT2H" }
            };

            var json = JsonConvert.SerializeObject(indexer);
            r.AddJsonBody(indexer);

            var response = c.Execute(r);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Something failed -  StatusCode: {response.StatusCode}");

        }
        private void DeleteExistingResources()
        {
            try
            {
                searchClient.Indexes.Delete(indexName);
                searchClient.DataSources.Delete(dataSourceName);
                searchClient.Indexers.Delete(indexerName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CreateIndex()
        {
            var definition = new Index()
            {
                Name = indexName,
                Fields = new[]
                    {
                        new Field("Useruuid",     DataType.String)         { IsKey = true,  IsSearchable = false, IsFilterable = false, IsSortable = false, IsFacetable = false, IsRetrievable = true},
                        new Field("Gender",     DataType.String)         { IsKey = false,  IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false, IsRetrievable = true},
                        new Field("Age",     DataType.String)         { IsKey = false,  IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false, IsRetrievable = true},
                        new Field("country",     DataType.String)         { IsKey = false,  IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false, IsRetrievable = true},
                        new Field("signup",     DataType.String)         { IsKey = false,  IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false, IsRetrievable = true},

                    }
            };

            searchClient.Indexes.Create(definition);
        }
        private DataSource CreateDataSource()
        {
            var dataSource = DataSource.AzureBlobStorage(dataSourceName,
                "DefaultEndpointsProtocol=https;AccountName=xxxxx;AccountKey=ez9t00eebp5UxWHFDxsVCwlASN1yf8Fd0QFiK8qgqsoRyX7oasJLDFP3xkgRMiKsSmjamv3OGpndTDrAO8g==;EndpointSuffix=core.windows.net", "search");

            try
            {
                searchClient.DataSources.Create(dataSource);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return dataSource;
        }
        private void CreateIndexer(DataSource dataSource, Action<string> logger)
        {
            var indexingParameters = new Dictionary<string, object>();
            indexingParameters.Add("parsingMode", "jsonArray");

            var indexer =
                new Indexer()
                {
                    Name = indexerName,
                    Description = "SimpleCLub",
                    DataSourceName = dataSource.Name,
                    TargetIndexName = "simpleclub",
                    Parameters = new IndexingParameters() { Configuration = indexingParameters }


                };

            try
            {
                searchClient.Indexers.Create(indexer);
            }
            catch (Exception ex)
            {
                logger(ex.Message);
            }
        }
    }
}
