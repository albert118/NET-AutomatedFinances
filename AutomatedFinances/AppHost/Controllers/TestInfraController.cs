using AutomatedFinances.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppHost.Controllers;

[ApiController]
[Route("[controller]")]
public class TestInfraController : ControllerBase
{
    private readonly ITradingTransactionReadDbContext _context;
    private readonly ILogger<TestInfraController> _logger;

    public TestInfraController(ILogger<TestInfraController> logger, ITradingTransactionReadDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetInfraTestResponse")]
    public IEnumerable<InfraTestResponse> Get()
    {
        _logger.LogInformation("Hit TestInfraController");

        var data = _context.GenericTransactions.ToList();

        return data.Select(d => new InfraTestResponse(d.TrackingId));
    }

    public sealed record InfraTestResponse(Guid Pk);
}
