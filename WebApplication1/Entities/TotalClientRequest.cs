using System.ComponentModel.DataAnnotations;
using System.Text.Json;

public class TotalClientRequest
{
    public int Id { get; set; } 
    public string Title { get; set; } = string.Empty; 
    public List<CoverageRequest> CoverageRequests { get; set; } = new(); 

    public decimal TotalAmount { get; set; } 
    public decimal TotalPremium { get; set; } 

    public class CoverageRequest
    {
        public int CoverageId { get; set; } // ID of the actual coverage
        public decimal RequestedAmount { get; set; } // Requested amount
    }
}
