using Microsoft.EntityFrameworkCore;
using IpLookup.Api.Data;
using IpLookup.Api.Interfaces;
using IpLookup.Api.Models.Entities;

namespace IpLookup.Api.Repositories
{
    public class IpRepository : IIpRepository
    {
        private readonly AppDbContext _context;

        public IpRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<IpQuery>> GetAllAsync()
        {
            return await _context.IpQueries
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IpQuery?> GetByIpAsync(string ipAddress)
        {
            return await _context.IpQueries
                .FirstOrDefaultAsync(x => x.IpAddress == ipAddress);
        }

        public async Task AddAsync(IpQuery ipQuery)
        {
            await _context.IpQueries.AddAsync(ipQuery);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.IpQueries.FindAsync(id);

            if (entity != null)
            {
                _context.IpQueries.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
