using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RecoOutputter
{
    public class AzureRecoOutputter : IProcessor
    {
        string _indexName = String.Empty;
        
        List<IRow> rows = null;

        #region Ctor
        public AzureRecoOutputter(string serviceName, string accountKey, string indexName, bool deleteIndex)
        {
           
            rows = new List<IRow>();
        }

        #endregion

      
        public override IRow Process(IRow input, IUpdatableRow output)
        {
            var foo = input;
            return input;
        }
    }
}