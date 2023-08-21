using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class MeritPayService:CrudBaseService<Base_MeritPay>
{
    public MeritPayService(IRepository<Base_MeritPay> repository) : base(repository)
    {
    }
}