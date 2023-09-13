using System;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoyAdmin.Core.Entities.Storage;

public class UploadData : EntityBase, IEntityTypeBuilder<UploadData>
{
    public string Code { get; set; }
    public int Order { get; set; }
    public string Name { get; set; }
    public string Result { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    
    public string Model { get; set; }

    public string WorkOrder { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime Time { get; set; } = DateTime.Now;
    
    

    public void Configure(EntityTypeBuilder<UploadData> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
        entityBuilder.HasIndex(x => x.Code);
        entityBuilder.HasIndex(x => x.Name);
    }
}