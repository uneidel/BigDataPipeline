using ContosoPoC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.cs
{
    class Program
    {
        

        static void Main(string[] args)
        {
            string key = "063E29A00269904714A006EFE246EAD1";
            string serviceName = "uneidel";
            string indexName = "foofighter";

            bool recreate = true;

            Functions r = new Functions(serviceName, key);
            
            if (!r.GetIndex(indexName) && recreate)
            {
               
                Console.WriteLine($"Index: {indexName} exists.");
                Console.Write($"Deleting Index: {indexName}: ");

                Console.WriteLine(r.DeleteIndex(indexName));
                Console.Write($"Creating Index: {indexName}: ");
                Console.WriteLine(r.CreateIndex(indexName));
            }

            r.AddDocument(indexName);
            Console.ReadKey();
        }
    }
}
