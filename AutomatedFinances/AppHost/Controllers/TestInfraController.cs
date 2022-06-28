using AutomatedFinances.Application.TradingManager;
using Microsoft.AspNetCore.Mvc;

namespace AppHost.Controllers;

[ApiController]
[Route("[controller]")]
public class TestInfraController : ControllerBase
{
    private readonly ITradingCommandService _commandService;
    private readonly ILogger<TestInfraController> _logger;
    private readonly ITradingQueryService _queryService;

    public TestInfraController(
        ILogger<TestInfraController> logger,
        ITradingQueryService queryService,
        ITradingCommandService commandService)
    {
        _logger = logger;
        _queryService = queryService;
        _commandService = commandService;
    }

    [HttpGet(Name = "GET InfraTest")]
    public IEnumerable<InfraTestResponse> Get()
    {
        _logger.LogInformation("Hit GET TestInfra Controller");
        return _queryService.GetAllTrades().Select(t => new InfraTestResponse(t.Pk, t.atDateTime));
    }

    [HttpPost(Name = "POST Add Entity InfraTest")]
    public async Task<bool> Post()
    {
        _logger.LogInformation("Hit POST Add Entity InfraTest Controller");

        var ctSource = new CancellationTokenSource();

        return await _commandService.AddTrade("me", "you", DateTime.UtcNow, ctSource.Token);
    }

    public sealed record InfraTestResponse(Guid Pk, DateTime atDateTime);
}
