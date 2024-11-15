using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entities;

namespace WebApplication1.Services
{
    public class InsuranceCoverageService : IInsuranceCoverageService
    {
        private readonly InsuranceContext _context;

        public InsuranceCoverageService(InsuranceContext context)
        {
            _context = context;
        }

        public async Task<InsuranceCoverage> GetCoverageByIdAsync(int coverageId)
        {
            var coverage = await _context.insuranceCoverages
                .Where(i => i.Id == coverageId).FirstOrDefaultAsync();
                
            return coverage;
        }
    }
}
