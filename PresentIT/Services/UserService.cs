using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentIT.Services
{
    public class UserService
    {

        private readonly PITContext _context;

        public UserService(PITContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExistsAsync(string Auth0Id)
        {
            return await _context.Candidate.FirstOrDefaultAsync(x=>x.Auth0 == Auth0Id) != null;
        }

        public async Task<Guid> GetUserIDAsync(string Auth0Id)
        {
            return (await _context.Candidate.FirstOrDefaultAsync(x=>x.Auth0 == Auth0Id)).Id;
        }
    }
}
