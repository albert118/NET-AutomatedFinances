namespace AutomatedFinances.Application.TransactionRecord.Dtos;

public record TransactionRecord(string Name, long TotalCost, DateTime OccuredAtDateTime, DateTime SavedAtDateTime);
