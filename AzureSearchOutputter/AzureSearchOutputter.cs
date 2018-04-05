using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace ContosoPoC
{
    //[SqlUserDefinedOutputter(AtomicFileProcessing = true)]
    public class AzureSearchOutputter : IProcessor
    {
        string _indexName = String.Empty;
        int rowBuffer = 20;
        Functions functions = null;
        List<dynamic> rows = null;
        bool indexAvailable = false;

        #region Ctor
        public AzureSearchOutputter(string serviceName,string accountKey,string indexName, bool deleteIndex)
        {   
            
           functions = new Functions(serviceName,accountKey);
            if (deleteIndex && functions.GetIndex(indexName))
                functions.DeleteIndex(indexName);
           
            this._indexName = indexName;
            rows = new List<dynamic>();
        }
        #endregion

       

        public override IRow Process(IRow input, IUpdatableRow output)
        {
            if (!indexAvailable)
            {
                functions.CreateIndex(this._indexName, input);
                indexAvailable = true;
            }

            if (rows.Count < rowBuffer)
                rows.Add(input.ToExpando());
            else
            {
                //Submit Rows
                functions.AddDocument(this._indexName, rows);
                rows.Clear();
            }
            
            return input;
        }
    }
}
