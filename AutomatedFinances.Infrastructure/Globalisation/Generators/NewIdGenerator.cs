using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace AutomatedFinances.Infrastructure.Globalisation.Generators;

internal sealed class NewIdGenerator : ValueGenerator<Guid>
{
    public override bool GeneratesTemporaryValues => false;
    public override Guid Next(EntityEntry entry) => Guid.NewGuid();
}