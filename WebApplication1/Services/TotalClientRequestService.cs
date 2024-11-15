using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entities;

namespace WebApplication1.Services
{
    public class TotalClientRequestService : ITotalClientRequestService
    {
        private InsuranceContext _context;

        public TotalClientRequestService(InsuranceContext context)
        {
            _context = context;
        }

        public async Task AddTotalCilentRequest(TotalClientRequest clientRequest)
        {
            await _context.totalClientRequests.AddAsync(clientRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TotalClientRequest>> GetAllTotalCilentRequests()
        {
            return await _context.totalClientRequests.ToListAsync();
        }
    }
}
