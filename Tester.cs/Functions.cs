using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ContosoPoC
{
    public class Functions
    {
        HttpClient _httpClient;
        Uri baseUri = null;
        private static readonly string apiversion = "2015-02-28-Preview";
        string _accountKey = String.Empty;
        public Functions(string serviceName, string accountKey)
        {
            this.baseUri = new Uri($"https://{serviceName}.search.windows.net/");
            this._accountKey = accountKey;
        }

        #region Done
        internal HttpClient client
        {
            get
            {
                if (_httpClient == null)
                 _httpClient = new HttpClient
                {
                    BaseAddress = this.baseUri,
                    Timeout = TimeSpan.FromMinutes(15),
                    DefaultRequestHeaders =
                  {
                      {"api-key", this._accountKey}
                  }
                };
                return _httpClient;
            }
        }
        /// <summary>
        /// Get Index
        /// </summary>
        /// <see cref="https://docs.microsoft.com/de-de/rest/api/searchservice/get-index"/>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool GetIndex(string index)
        {
            string query = $"/indexes/{index}?api-version={apiversion}";
            var response = client.GetAsync(query).Result;
            return response.IsSuccessStatusCode;
        }
        /// <summary>
        /// Create Index
        /// </summary>
        /// <see cref="https://docs.microsoft.com/de-de/rest/api/searchservice/create-index"/>
        /// <returns></returns>
        public bool CreateIndex(string indexName)
        {
            string query = $"/indexes/{indexName}?api-version={apiversion}";
            var cnt = new StringContent("", Encoding.UTF8, "application/json");
            var response = client.PutAsync(query,cnt).Result;
            return response.IsSuccessStatusCode;
        }
        /// <summary>
        /// Delete an existing Index
        /// </summary>
        /// <see cref="https://docs.microsoft.com/de-de/rest/api/searchservice/delete-index"/>
        /// <returns></returns>
        public bool DeleteIndex(string index)
        {
            string query = $"/indexes/{index}?api-version={apiversion}";
            var response = client.DeleteAsync(query).Result;
            return response.IsSuccessStatusCode;
        }
        private string CreateUserIndexBody(string indexName)
        {
            var index = new Index();
            index.fields = new List<Field>();
            
            index.name = indexName;
            index.fields.Add(new Field() { name = "useruuid", type = "Edm.String", key = true, searchable= true });
            
            index.fields.Add(new Field() { name = "gender", type = "Edm.String" });
            index.fields.Add(new Field() { name = "Age", type = "Edm.Int32"});
            index.fields.Add(new Field() { name = "country", type = "Edm.String"});
            index.fields.Add(new Field() { name = "signup", type = "Edm.String" });
            
            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            string json = JsonConvert.SerializeObject(index, settings);
            return json;
        }
        /// <summary>
        /// Add or Update Daocuments
        /// </summary>
        /// <see cref="https://docs.microsoft.com/de-de/rest/api/searchservice/addupdate-or-delete-documents"/>
        public bool AddDocument(string indexName)
        {
            string query = $"/indexes/{indexName}/docs/index?api-version={apiversion}";

            var doc = (dynamic)new ExpandoObject();
            doc.value = new List<dynamic>();
            
            var row = (dynamic)new ExpandoObject();
            
            row.searchaction = "upload";
            row.useruuid = "49f2b518-8615-473b-9857-a1847c797798";
            row.gender = "m";
            row.Age = 24;
            row.country = "Germany";
            row.signup = DateTime.Now.ToShortDateString();

            doc.value.Add(row);


            var json = JsonConvert.SerializeObject(doc, 
                                                    Formatting.Indented,
                                                    new JsonSerializerSettings { ContractResolver = new DotheAtThingContractResolver() });
            var cnt = new StringContent(json, Encoding.UTF8, "application/json");
            var response = client.PostAsync(query, cnt).Result;
            return response.IsSuccessStatusCode;
        }



        #endregion



        #region CustomContractResolver

        public class DotheAtThingContractResolver : DefaultContractResolver
        {
            public new static readonly DotheAtThingContractResolver Instance = new DotheAtThingContractResolver();

            protected override string ResolvePropertyName(string propertyName)
            {
                var name = base.ResolvePropertyName(propertyName);
                if (propertyName == "searchaction")
                        name =  "@search.action";
                return name;
            }
        }

        #endregion 

        #region Models
        public class Field
        {
            public string name { get; set; }
            public string type { get; set; }
            public bool key { get; set; }
            public bool searchable { get; set; }
            public bool? filterable { get; set; }
            public bool? sortable { get; set; }
            public bool? facetable { get; set; }
            public string analyzer { get; set; }
        }

        public class Suggester
        {
            public string name { get; set; }
            public string searchMode { get; set; }
            public List<string> sourceFields { get; set; }
        }

        public class Analyzer
        {
            public string name { get; set; }
            public List<string> charFilters { get; set; }
            public string tokenizer { get; set; }
        }

        public class Index
        {
            public Index()
            {
                
            }
            public string name { get; set; }
            public List<Field> fields { get; set; }
            public List<Suggester> suggesters { get; set; }
            public List<Analyzer> analyzers { get; set; }
        }


    #endregion
}
}
