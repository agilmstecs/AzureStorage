using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Linq;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Azure;

namespace storageDemop
{
    public static class DbRepository<T> where T : class
    {
        private static readonly string dbId = CloudConfigurationManager.GetSetting("databaseId");
        private static readonly string collectionId = CloudConfigurationManager.GetSetting("collectionId");
        private static DocumentClient client;

        public static void Initialize()
        {
            client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["uri"]), ConfigurationManager.AppSettings["authenticationkey"]);

        }

        public static async Task<IEnumerable<T>> GetItemsAsync()
        {
            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(UriFactory.CreateCollectionUri(dbId, collectionId)).AsDocumentQuery();
            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }
            return results;
        }


        public static async Task<Document> CreateItemAsync(T item)
        {
            return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbId, collectionId), item);
        }
    }
}