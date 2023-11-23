using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Production;

namespace JoyAdmin.Application.Production;

public class ProductRecordService:ServiceBase<Production_ProductRecord>
{
    public ProductRecordService(IRepository<Production_ProductRecord> repository) : base(repository)
    {
        
    }
    
}