using System;

namespace AutomatedFinances.Infrastructure.Globalisation;

public interface IHaveUtcTimeStamp
{
    DateTime OccuredAtDateTime { get; set; }

    DateTime RecordedAtDateTime { get; set; }
}