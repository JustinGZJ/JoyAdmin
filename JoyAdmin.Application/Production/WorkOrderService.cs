using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using JoyAdmin.Application.Custom;
using JoyAdmin.Core.Entities.Custom;
using JoyAdmin.Core.Entities.Production;
using JoyAdmin.Core.Entities.Storage;
using Microsoft.EntityFrameworkCore;
using Base_ProcessLine = JoyAdmin.Core.Entities.Custom.Base_ProcessLine;

namespace JoyAdmin.Application.Production;

public class WorkOrderService : ServiceBase<ProductionWorkOrder>
{
    private readonly IRepository<ProductionWorkOrder> _workorderRepository;
    private readonly IRepository<Core.Entities.Storage.Production> _productionRepository;
    private readonly IRepository<Base_Product> _productRepository;
    private readonly IRepository<Base_ProcessLine> _processLineRepository;
    private readonly IRepository<Base_ProcessLineList> _processLineListRepository;
    private readonly IRepository<Base_Process> _processRepository;

    public WorkOrderService(
        IRepository<ProductionWorkOrder> workorderRepository,
        IRepository<Core.Entities.Storage.Production> productionRepository,
        IRepository<Base_Product> productRepository,
        IRepository<Base_ProcessLine> processLineRepository,
        IRepository<Base_ProcessLineList> processLineListRepository,
        IRepository<Base_Process> processRepository) : base(workorderRepository)
    {
       _workorderRepository = workorderRepository;
        _productionRepository = productionRepository;
        _productRepository = productRepository;
        _processLineRepository = processLineRepository;
        _processLineListRepository = processLineListRepository;
        _processRepository = processRepository;
    }

    public async Task UpdateActiveWorkOrder()
    {
        var orders = await _workorderRepository.Entities.Where(x => x.Status == "进行中").ToListAsync();
        // 查询工单关联的产品的工艺路线
        var productNos = orders.Select(x => x.ProductNo).ToList();
        var products = await _productRepository.Entities.Where(x => productNos.Contains(x.ProductCode)).ToListAsync();
        foreach (var order in orders)
        {
            var product = products.FirstOrDefault(x => x.ProductCode == order.ProductNo);
            var processLineId = product?.ProcessLine_Id;
            var processLine =
                await _processLineRepository.Entities.FirstOrDefaultAsync(x => x.ProcessLine_Id == processLineId);
            if (processLine != null)
            {
                await UpdateWorkOrder(processLine, order);
            }
        }
    }

    private async Task UpdateWorkOrder(Base_ProcessLine processLine, ProductionWorkOrder order)
    {
        var processLineLists = await _processLineListRepository.Entities
            .Where(x => x.ProcessLine_Id == processLine.ProcessLine_Id).OrderBy(x => x.Sequence).ToListAsync();
        // 判断最后的processLineList的
        var lastProcessLineList = processLineLists.LastOrDefault();
        if (lastProcessLineList != null)
        {
            //如果是工艺路线则查询最后一个工位的产量，如果是工序则查询工序的产量
            var lastProcessLineListType = lastProcessLineList.ProcessLineType;
            switch (lastProcessLineListType)
            {
                case "工艺路线":
                    // 找到下行工艺路线
                    var downProcessLine = await _processLineRepository.Entities.FirstOrDefaultAsync(x =>
                        x.ProcessLine_Id == lastProcessLineList.ProcessLineDown_Id);
                    if (downProcessLine != null)
                        await UpdateWorkOrder(downProcessLine, order);
                    break;

                case "工序":
                    var lastProcess =
                        await _processRepository.Entities.FirstOrDefaultAsync(x =>
                            x.Process_Id == lastProcessLineList.Process_Id);
                    if (lastProcess != null)
                    {
                        var processName = lastProcess.ProcessName;
                        //根据工单号和工序名称查询产量
                        var productions = _productionRepository.Entities
                            .Where(x => x.WorkOrderNo == order.WorkOrderNo && x.Device == processName).ToList();
                        // 根据productiontype 统计 OK NG FEED
                        var okCount = productions.Count(x => x.ProductionType == ProductionType.OK);
                        var ngCount = productions.Count(x => x.ProductionType == ProductionType.NG);
                        var feedCount = productions.Count(x => x.ProductionType == ProductionType.FEED);
                        order.NgQuantity = ngCount;
                        order.ActualQuantity = okCount;
                        await _workorderRepository.UpdateNowAsync(order);
                    }

                    break;
            }
        }
    }
}