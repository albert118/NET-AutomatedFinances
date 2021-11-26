using System.Data;

namespace AutomatedFinances.Application.Interfaces
{
    public interface IAutomedFinancesDbContext
    {
        // base data source connection for read/write
        IDbConnection GetWriterConnection();
    }
}
