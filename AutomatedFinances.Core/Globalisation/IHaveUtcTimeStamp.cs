namespace AutomatedFinances.Core.Globalisation;

public interface IHaveUtcTimeStamp
{
    DateTime OccuredAtDateTime { get; set; }

    DateTime RecordedAtDateTime { get; set; }
}