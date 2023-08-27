using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using JoyAdmin.Core.Entities.Production;

namespace JoyAdmin.Application.Production;

public class WorkOrderService:ServiceBase<ProductionWorkOrder>
{
    public WorkOrderService(IRepository<ProductionWorkOrder> repository) : base(repository)
    {
    }
}


// public class WorkOrderService:IDynamicApiController
// {
//     private readonly IRepository<ProductionWorkOrder> _repository;
//
//     public WorkOrderService(IRepository<ProductionWorkOrder> repository)
//     {
//         _repository = repository;
//     }
//     
//     public async Task<PagedList<ProductionWorkOrder>> GetWorkOrders(int pageIndex = 1, int pageSize = 50)
//     {
//         return await _repository.Entities.ToPagedListAsync(pageIndex, pageSize);
//     }
//     
//     public async Task<ProductionWorkOrder> GetWorkOrderById(int id)
//     {
//         return await _repository.FindAsync(id);
//     }
//     
//     public  Task AddWorkOrder(ProductionWorkOrder workOrder)
//     {
//         return _repository.InsertNowAsync(workOrder);
//     }
//     
//     public Task UpdateWorkOrder(ProductionWorkOrder workOrder)
//     {
//         return _repository.UpdateAsync(workOrder);
//     }
//     
//     public Task DeleteWorkOrder(int id)
//     {
//         return _repository.DeleteAsync(id);
//     }
// }