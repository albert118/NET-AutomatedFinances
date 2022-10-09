using AutomatedFinances.Application.TransactionRecord;
using AutomatedFinances.Application.TransactionRecord.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AppHost.Controllers;

public class SubmitFormDataRequest {
    public string Description { get; set;}
    public long TotalCost { get; set;}
    public DateTime Date { get; set;}
    public string Reference { get; set;}
}

[ApiController]
[Route("[controller]")]
public class TransactionRecordController : ControllerBase
{
    private readonly ILogger<TransactionRecordController> _logger;
    private readonly ITransactionRecordCommandService _commandService;
    private readonly ITransactionQueryService _queryService;

    public TransactionRecordController(ILogger<TransactionRecordController> logger, ITransactionRecordCommandService
    commandService, ITransactionQueryService queryService) {
        _logger = logger;
        _commandService = commandService;
        _queryService = queryService;
    }

    [HttpPost(Name = "POST Creating a new (Financial) Transaction Record")]
    public async Task<bool> Post(SubmitFormDataRequest request)
    {
        var ctSource = new CancellationTokenSource();

        return await _commandService.AddTransaction(
            request.Description,
            request.TotalCost,
            request.Date,
            request.Reference,
            ctSource.Token
        );
    }

    [HttpGet(Name = "GET All (financial) transaction records")]
    public List<TransactionRecord> Get() => _queryService.GetAllTransactionRecords().ToList();
}
