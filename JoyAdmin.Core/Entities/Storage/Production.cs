﻿using System;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoyAdmin.Core.Entities.Storage;
public enum ProductionType
{
    // 进料
    FEED,
    // OK
    OK,
    // NG
    NG
}

public class Production : EntityBase, IEntityTypeBuilder<Production>
{

    public string Device { get; set; }
    public  ProductionType ProductionType { get; set; }
    public int Quantity { get; set; }
    public string Reason { get; set; }
    public int Count { get; set; }
    public DateTime Time { get; set; }=DateTime.Now;
    public void Configure(EntityTypeBuilder<Production> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
        //  throw new NotImplementedException();
        entityBuilder.HasIndex(x => x.ProductionType);
        entityBuilder.HasIndex(x => x.Device);
    }
}