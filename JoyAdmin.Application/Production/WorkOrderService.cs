using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Production;

namespace JoyAdmin.Application.Production;

public class WorkOrderService : ServiceBase<ProductionWorkOrder>
{
    public WorkOrderService(IRepository<ProductionWorkOrder> repository) : base(repository)
    {
    }
}