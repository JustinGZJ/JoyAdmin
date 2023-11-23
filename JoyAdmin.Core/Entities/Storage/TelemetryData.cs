using System;
using System.Text.Json;
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

    public JsonDocument Value { get; set; }

    public DateTime Time { get; set; } = DateTime.Now;

    public void Configure(EntityTypeBuilder<Telemetry> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
        entityBuilder.HasIndex(x => x.EntityName);
        entityBuilder.HasIndex(x => x.Key);
        entityBuilder.Property(x=>x.Value).HasColumnType("jsonb");
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