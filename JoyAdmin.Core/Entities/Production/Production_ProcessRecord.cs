using System;
using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoyAdmin.Core.Entities.Production;

/// <summary>
///  过程记录
/// </summary>
public class Production_ProcessRecord : EntityBase, IEntityTypeBuilder<Production_ProcessRecord>
{
    public DateTime EnterTime { get; set; }
    public DateTime LeaveTime { get; set; }
    public int? ProcessId { get; set; }
    public Base_Process Process { get; set; }
    public int? ProductRecordId { get; set; }

    public Production_ProductRecord ProductRecord { get; set; }

    // 生成数据ID
    public int DataId { get; set; }

    public bool Result { get; set; }

    public void Configure(EntityTypeBuilder<Production_ProcessRecord> entityBuilder, DbContext dbContext,
        Type dbContextLocator)
    {
        entityBuilder.ToTable("Production_ProcessRecord");
        entityBuilder.HasKey(k => k.Id);
        entityBuilder
            .HasOne(x => x.Process)
            .WithMany()
            .HasForeignKey(x => x.ProcessId);
        entityBuilder
            .HasOne(x => x.ProductRecord)
            .WithMany(x => x.ProcessRecords)
            .HasForeignKey(x => x.ProductRecordId);
    }
}

public class ProcessRecordDto
{
    public DateTime EnterTime { get; set; }
    public DateTime LeaveTime { get; set; }
    public string ProcessName { get; set; }
    // 生成数据ID
    public int DataId { get; set; }
    public bool Result { get; set; }
}