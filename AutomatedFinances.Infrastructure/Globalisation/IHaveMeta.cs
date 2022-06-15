using System;

namespace AutomatedFinances.Infrastructure.Globalisation;

public interface IHaveMeta
{
    DateTime SavedAtDateTime { get; set; }
    string? SavedBy { get; set; }
}