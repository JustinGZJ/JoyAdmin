using System;
using System.Linq;
using System.Threading.Tasks;
using Furion;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.TaskScheduler;
using JoyAdmin.Core.Entities.Custom;
using JoyAdmin.Core.Entities.Production;
using JoyAdmin.Core.Entities.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JoyAdmin.Application.Workers;

public class JobWorker : ISpareTimeWorker
{
    [SpareTime(5000, "jobName1", StartNow = true)]
    public void UpdateWorkOrder(SpareTimer timer, long count)
    {
        Scoped.Create(async (_, scope) =>
        {
            var services = scope.ServiceProvider;
            var workorderRepository = services.GetService<IRepository<ProductionWorkOrder>>();
            var productionRepository = services.GetService<IRepository<Core.Entities.Storage.Production>>();
            var productRepository = services.GetService<IRepository<Base_Product>>();
            var processLineRepository = services.GetService<IRepository<Base_ProcessLine>>();
            var processLineListRepository = services.GetService<IRepository<BaseProcessLineList>>();
            var processRepository = services.GetService<IRepository<Base_Process>>();
            await UpdateActiveWorkOrder();


            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine($"一共执行了：{count} 次");

            async Task UpdateActiveWorkOrder()
            {
                var orders = await workorderRepository.Entities.Where(x => x.Status == "进行中").ToListAsync();
                // 查询工单关联的产品的工艺路线
                var productNos = orders.Select(x => x.ProductNo).ToList();
                var products = await productRepository.Entities.Where(x => productNos.Contains(x.ProductCode))
                    .ToListAsync();
                foreach (var order in orders)
                {
                    var product = products.FirstOrDefault(x => x.ProductCode == order.ProductNo);
                    var processLineId = product?.ProcessLine_Id;
                    var processLine =
                        await processLineRepository.Entities.FirstOrDefaultAsync(
                            x => x.ProcessLine_Id == processLineId);
                    if (processLine != null)
                    {
                        await UpdateWorkOrder(processLine, order);
                    }
                }
            }

            async Task UpdateWorkOrder(Base_ProcessLine processLine, ProductionWorkOrder order)
            {
                var processLineLists = await processLineListRepository.Entities
                    .Where(x => x.ProcessLineId == processLine.ProcessLine_Id).OrderBy(x => x.Sequence).ToListAsync();
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
                            var downProcessLine = await processLineRepository.Entities.FirstOrDefaultAsync(x =>
                                x.ProcessLine_Id == lastProcessLineList.ProcessLineDown_Id);
                            if (downProcessLine != null)
                                await UpdateWorkOrder(downProcessLine, order);
                            break;

                        case "工序":
                            var lastProcess =
                                await processRepository.Entities.FirstOrDefaultAsync(x =>
                                    x.Process_Id == lastProcessLineList.Process_Id);
                            if (lastProcess != null)
                            {
                                var processName = lastProcess.ProcessName;
                                //根据工单号和工序名称查询产量
                                var productions = productionRepository.Entities
                                    .Where(x => x.WorkOrderNo == order.WorkOrderNo && x.Device == processName).ToList();
                                // 根据productiontype 统计 OK NG FEED
                                var okCount = productions.Count(x => x.ProductionType == ProductionType.OK);
                                var ngCount = productions.Count(x => x.ProductionType == ProductionType.NG);
                                var feedCount = productions.Count(x => x.ProductionType == ProductionType.FEED);
                                order.NgQuantity = ngCount;
                                order.ActualQuantity = okCount;
                                await workorderRepository.UpdateNowAsync(order);
                            }

                            break;
                    }
                }
            }
        });
    }
}