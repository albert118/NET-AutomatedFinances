namespace AutomatedFinances.Core.Globalisation;

public interface IHaveMeta
{
    DateTime SavedAtDateTime { get; set; }
    string? SavedBy { get; set; }
}