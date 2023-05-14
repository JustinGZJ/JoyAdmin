using JoyAdmin.Core.Entities.Storage;

namespace JoyAdmin.Application.Statistic.Dtos;

public class ProductionDto
{
    public string Device { get; set; }
    public  ProductionType ProductionType { get; set; }
    public int Quantity { get; set; }
    public string Reason { get; set; }
    public int Count { get; set; }
}