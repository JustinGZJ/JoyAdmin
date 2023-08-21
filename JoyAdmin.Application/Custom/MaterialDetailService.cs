using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class MaterialDetailService:CrudBaseService<Base_MaterialDetail>
{
    public MaterialDetailService(IRepository<Base_MaterialDetail> repository) : base(repository)
    {
    }
}