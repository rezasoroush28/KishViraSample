using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Models;
using WebApplication1.Services;

[ApiController]
[Route("api/[controller]")]
public class TotalClientRequestController : ControllerBase
{
    private readonly IInsuranceCoverageService _coverageService;
    private readonly ITotalClientRequestService _totalClientRequestService;

    public TotalClientRequestController(IInsuranceCoverageService coverageService, ITotalClientRequestService totalClientRequestService)
    {
        _coverageService = coverageService;
        _totalClientRequestService = totalClientRequestService;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitCoverageRequest(string title, [FromBody] List<CoverageRequestModel> coverageRequests)
    {
        if (string.IsNullOrEmpty(title))
            return BadRequest("Title is required.");

        if (coverageRequests == null || !coverageRequests.Any())
            return BadRequest("At least one coverage request is required.");

        var totalClientRequest = new TotalClientRequest();
        totalClientRequest.CoverageRequests = new List<TotalClientRequest.CoverageRequest>();

        foreach (var request in coverageRequests)
        {
            var coverage = await _coverageService.GetCoverageByIdAsync(request.CoverageId);
            if (coverage == null)
                return BadRequest("Coverage ID not found.");

            if (request.RequestedAmount < coverage.MinimumAmount || request.RequestedAmount > coverage.MaximumAmount)
                return BadRequest($"Requested amount for {coverage.Type} is out of range.");
               
            var premium = CalculatePremium(coverage.Type, request.RequestedAmount);
            totalClientRequest.TotalAmount += request.RequestedAmount;
            totalClientRequest.TotalPremium += premium;
            totalClientRequest.CoverageRequests.Add(new TotalClientRequest.CoverageRequest
            {
                CoverageId = request.CoverageId,
                RequestedAmount = request.RequestedAmount
            });
        }

        _totalClientRequestService.AddTotalCilentRequest(totalClientRequest);

        return Ok("Request submitted successfully ");
    }

    public async Task<List<TotalClientRequest>> GetAllClientRequests()
    {
        return await _totalClientRequestService.GetAllTotalCilentRequests();
    }
    private decimal CalculatePremium(string coverageType, decimal requestedAmount)
    {
        return coverageType switch
        {
            "Surgery" => requestedAmount * 0.0052M,
            "Dentistry" => requestedAmount * 0.0042M,
            "Hospitalization" => requestedAmount * 0.005M,
            _ => 0
        };
    }
}
