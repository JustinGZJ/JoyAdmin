using System;
using Furion.DatabaseAccessor;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoyAdmin.Core.Entities.Storage;

public class AlarmHistory : EntityBase,IEntityTypeBuilder<AlarmHistory>
{
    public string Station { get; set; } = "";
    public string Address { get; set; }
    public string Message { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public void Configure(EntityTypeBuilder<AlarmHistory> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
        entityBuilder.HasIndex(x => x.StartTime);
        entityBuilder.HasIndex(x => x.EndTime);
        entityBuilder.HasIndex(x => x.Station);
    }
}