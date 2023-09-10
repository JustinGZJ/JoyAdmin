using System;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoyAdmin.Core.Entities.Storage;

public class Telemetry : IEntity, IEntityTypeBuilder<Telemetry>
{
    public long Id { get; set; }

    public string EntityName { get; set; }

    public DataType DataType { get; set; } = DataType.Json;

    public string Key { get; set; }

    public double? ValueNum { get; set; }

    public bool? ValueBool { get; set; }

    public string ValueStr { get; set; }

    public string ValueJson { get; set; }

    public DateTime Time { get; set; } = DateTime.Now;

    public void Configure(EntityTypeBuilder<Telemetry> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
        entityBuilder.HasIndex(x => x.EntityName);
        entityBuilder.HasIndex(x => x.Key);
    }
}

public class TelemetryData : Telemetry
{
}

public class TelemetryLatest : Telemetry
{
}

public enum DataType
{
    Json,
    Number,
    Boolean,
    String,
}