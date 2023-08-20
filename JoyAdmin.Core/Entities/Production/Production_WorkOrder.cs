using System;
using Furion.DatabaseAccessor;

namespace JoyAdmin.Core.Entities.Production;

public class ProductionWorkOrder:Entity
{
    public string WorkOrderNo { get; set; } // 工单号
    public string ProductNo { get; set; } // 产品编号
    public string ProductName { get; set; }
    public int PlanQuantity { get; set; } // 计划数量
    public int ActualQuantity { get; set; } // 实际数量
    public DateTime StartTime { get; set; } // 开始时间
    public DateTime FinishTime { get; set; } // 完成时间
    public string Status { get; set; } // 工单状态
}