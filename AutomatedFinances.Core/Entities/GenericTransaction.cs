using AutomatedFinances.Core.Globalisation;

namespace AutomatedFinances.Core.Entities;

public class GenericTransaction : IHaveUtcTimeStamp, IHaveMeta
{
    public Guid TrackingId { get; set; }

    public string From { get; set; }

    public string To { get; set; }

    #region IHaveUtcTimeStamp

    public DateTime OccuredAtDateTime { get; set; }

    public DateTime RecordedAtDateTime { get; set; }

    #endregion

    #region IHaveMeta

    public DateTime SavedAtDateTime { get; set; }

    public string? SavedBy { get; set; }

    #endregion
}