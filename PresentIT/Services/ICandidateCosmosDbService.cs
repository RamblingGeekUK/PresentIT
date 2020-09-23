using System.Collections.Generic;
using System.Threading.Tasks;
using PresentIT.Models;

namespace PresentIT.Services
{
    public interface ICandidateCosmosDbService
    {
        Task<IEnumerable<Candidate>> GetItemsAsync(string query);
        Task<Candidate> GetItemAsync(string id);
        Task AddItemAsync(Candidate item);
        Task UpdateItemAsync(string id, Candidate item);
        Task DeleteItemAsync(string id);
    }
}
