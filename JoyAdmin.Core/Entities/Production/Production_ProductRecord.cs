using System;
using System.Collections.Generic;
using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoyAdmin.Core.Entities.Production;

/// <summary>
/// 产品的状态记录
/// </summary>
public class Production_ProductRecord:EntityBase,IEntityTypeBuilder<Production_ProductRecord>
{
    // 产品条码
    public string BarCode { get; set; }
    // 产品ID
    public int? ProductId { get; set; }
    public Base_Product Product { get; set; }
    // 当前工序ID
    public int? CurrentProcessId { get; set; }
    public Base_Process CurrentProcess { get; set; }

    public string Status { get; set; } = "";
    
   public List<Production_ProcessRecord> ProcessRecords { get; set; }
    public void Configure(EntityTypeBuilder<Production_ProductRecord> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
        entityBuilder.ToTable("Production_ProductRecord");
        entityBuilder.HasKey(k => k.Id);
        entityBuilder
            .HasOne(x=>x.Product)
            .WithMany()
            .HasForeignKey(x=>x.ProductId);
        entityBuilder
            .HasOne(x => x.CurrentProcess)
            .WithMany()
            .HasForeignKey(x => x.CurrentProcessId);
        
        entityBuilder
            .HasMany(x => x.ProcessRecords)
            .WithOne(x => x.ProductRecord)
            .HasForeignKey(x => x.ProductRecordId);
    }
}