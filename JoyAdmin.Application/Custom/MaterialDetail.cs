using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class MaterialDetail : ServiceBase<Base_MaterialDetail>
{
    public MaterialDetail(IRepository<Base_MaterialDetail> workorderRepository) : base(workorderRepository)
    {
    }
}