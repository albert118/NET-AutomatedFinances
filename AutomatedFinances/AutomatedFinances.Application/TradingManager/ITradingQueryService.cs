using AutomatedFinances.Application.TradingManager.Dtos;

namespace AutomatedFinances.Application.TradingManager;

public interface ITradingQueryService
{
    public IEnumerable<TradingInfo> GetAllTrades();
}