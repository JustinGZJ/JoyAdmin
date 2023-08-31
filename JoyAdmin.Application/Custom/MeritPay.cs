using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class MeritPay : ServiceBase<Base_MeritPay>
{
    public MeritPay(IRepository<Base_MeritPay> repository) : base(repository)
    {
    }
}