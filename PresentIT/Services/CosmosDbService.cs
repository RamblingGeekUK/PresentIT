using Microsoft.Azure.Cosmos;
using PresentIT.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentIT.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Company item)
        {
            await this._container.CreateItemAsync<Company>(item, new PartitionKey(item.Id));
        }

       

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Company>(id, new PartitionKey(id));
        }

        public async Task<Company> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Company> response = await this._container.ReadItemAsync<Company>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Company>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Company>(new QueryDefinition(queryString));
            List<Company> results = new List<Company>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, Company item)
        {
            await this._container.UpsertItemAsync<Company>(item, new PartitionKey(id));
        }
    }
}