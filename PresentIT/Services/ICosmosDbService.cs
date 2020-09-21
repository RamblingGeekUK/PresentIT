using System.Collections.Generic;
using System.Threading.Tasks;
using PresentIT.Models;

namespace PresentIT.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Company>> GetItemsAsync(string query);
        Task<Company> GetItemAsync(string id);
        Task AddItemAsync(Company item);
        Task UpdateItemAsync(string id, Company item);
        Task DeleteItemAsync(string id);
    }
}
