using WebApplication1.Entities;

namespace WebApplication1.Services
{
    public interface ITotalClientRequestService
    {
        Task AddTotalCilentRequest(TotalClientRequest clientRequest);
        Task<List<TotalClientRequest>> GetAllTotalCilentRequests();
    }

}
