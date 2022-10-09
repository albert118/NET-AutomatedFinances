using AutomatedFinances.Core.Globalisation;
using System.Globalization;

namespace AutomatedFinances.Core.Entities;

public class FinancialTransactionRecord : IHaveUtcTimeStamp, IHaveMeta
{
    public Guid Id { get; set; }

    public string Name => $"{Description} - {TransactionDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}";

    public string Description { get; set; } = string.Empty;

    public long TotalCost { get; set; }

    /// <summary>aka <see cref="OccuredAtDateTime"/></summary>
    public DateTime TransactionDate => OccuredAtDateTime;

    public string Reference { get; set; } = string.Empty;

    #region IHaveUtcTimeStamp

    public DateTime OccuredAtDateTime { get; set; }

    public DateTime RecordedAtDateTime { get; set; }

    #endregion

    #region IHaveMeta

    public DateTime SavedAtDateTime { get; set; }

    public string? SavedBy { get; set; }

    #endregion
}
