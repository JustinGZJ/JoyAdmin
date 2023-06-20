using System;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoyAdmin.Core.Entities.Storage;

public class ShellCodeBinding : EntityBase, IEntityTypeBuilder<ShellCodeBinding>
{
    public string ShellCode { get; set; }

    public string StatorCode { get; set; }

    public string RotorCode { get; set; }

    public void Configure(EntityTypeBuilder<ShellCodeBinding> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
        entityBuilder.HasIndex(x => x.ShellCode);
        entityBuilder.HasIndex(x => x.RotorCode);
        entityBuilder.HasIndex(x => x.StatorCode);
        //   throw new NotImplementedException();
    }

    public DateTime CreateTime { get; set; }
    public bool IsDeleted { get; set; }
}