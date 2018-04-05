using System;
using System.IO;
using System.Security.Cryptography.X509Certificates; // Required only if you are using an Azure AD application created with certificates
using System.Threading;

using Microsoft.Azure.Management.DataLake.Store;
using Microsoft.Azure.Management.DataLake.Store.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest.Azure.Authentication;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DataLakeTester
{
    class Program
    {
        private static DataLakeStoreAccountManagementClient _adlsClient;
        private static DataLakeStoreFileSystemManagementClient _adlsFileSystemClient;

        private static string _adlsAccountName;
        private static string _resourceGroupName;
        private static string _location;
        private static string _subId;

        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            try
            {
                _adlsAccountName = "uneidel"; // TODO: Replace this value with the name of your existing Data Lake Store account.
                _resourceGroupName = "Sample"; // TODO: Replace this value with the name of the resource group containing your Data Lake Store account.
                _location = "North Europe";
                _subId = "69edde7d-b2fe-4eae-911d-8a9a491bee69";

                string localFolderPath = @"C:\local_path\"; // TODO: Make sure this exists and can be overwritten.
                string localFilePath = Path.Combine(localFolderPath, "file.txt"); // TODO: Make sure this exists and can be overwritten.
                string remoteFolderPath = "/";
                string remoteFilePath = Path.Combine(remoteFolderPath, "ratings.dat");

                // Service principal / appplication authentication with client secret / key
                // Use the client ID of an existing AAD "Web App" application.
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

                var domain = "microsoft.onmicrosoft.com";
                var webApp_clientId = "cbf9fd14-9fcb-4316-8362-1b8a987d34cb";
                var clientSecret = "NsHKalzZYrOou0916K1971WY5DPWGiLTl5tElXLUBlQ=";
                var clientCredential = new ClientCredential(webApp_clientId, clientSecret);
                var creds = await ApplicationTokenProvider.LoginSilentAsync(domain, clientCredential);

                // Create client objects and set the subscription ID
                _adlsClient = new DataLakeStoreAccountManagementClient(creds) { SubscriptionId = _subId };
                _adlsFileSystemClient = new DataLakeStoreFileSystemManagementClient(creds);
                //var accs = await ListAdlStoreAccounts();
                DownloadFile(remoteFilePath, "c:\\temp\\foo1.txt");
            }
            catch(Exception ex)
            {
                var foo = ex;

            }
        }

        public static void DownloadFile(string srcFilePath, string destFilePath)
        {
            _adlsFileSystemClient.FileSystem.DownloadFile(_adlsAccountName, srcFilePath, destFilePath);
        }
        public static async Task<List<DataLakeStoreAccount>> ListAdlStoreAccounts()
        {
            var response = await _adlsClient.Account.ListAsync();
            var accounts = new List<DataLakeStoreAccount>(response);

            while (response.NextPageLink != null)
            {
                response = _adlsClient.Account.ListNext(response.NextPageLink);
                accounts.AddRange(response);
            }

            return accounts;
        }
    }
}
