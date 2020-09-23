using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using PresentIT.Models;

namespace PresentIT.Services
{
    public class CandidateCosmosDbService : ICandidateCosmosDbService
    {
        private Container _container;

        public CandidateCosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Candidate item)
        {
            await this._container.CreateItemAsync<Candidate>(item, new PartitionKey(item.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Candidate>(id, new PartitionKey(id));
        }

        public async Task<Candidate> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Candidate> response = await this._container.ReadItemAsync<Candidate>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }


        public async Task<IEnumerable<Candidate>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Candidate>(new QueryDefinition(queryString));
            List<Candidate> results = new List<Candidate>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, Candidate item)
        {
            await this._container.UpsertItemAsync<Candidate>(item, new PartitionKey(id));
        }
    }
}



