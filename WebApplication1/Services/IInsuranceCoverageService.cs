using WebApplication1.Entities;

namespace WebApplication1.Services
{
    public interface IInsuranceCoverageService
    {
        Task<InsuranceCoverage> GetCoverageByIdAsync(int coverageId);
    }
}
