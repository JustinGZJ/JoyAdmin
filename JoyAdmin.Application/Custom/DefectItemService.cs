using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class DefectItemService:CrudBaseService<Base_DefectItem>
{
    public DefectItemService(IRepository<Base_DefectItem> repository) : base(repository)
    {
    }
}

public class DesktopMenuService:CrudBaseService<Base_DesktopMenu>
{
    public DesktopMenuService(IRepository<Base_DesktopMenu> repository) : base(repository)
    {
    }
}

public class MaterialDetailService:CrudBaseService<Base_MaterialDetail>
{
    public MaterialDetailService(IRepository<Base_MaterialDetail> repository) : base(repository)
    {
    }
}

public class MeritPayService:CrudBaseService<Base_MeritPay>
{
    public MeritPayService(IRepository<Base_MeritPay> repository) : base(repository)
    {
    }
}

public class NoticeService:CrudBaseService<Base_Notice>
{
    public NoticeService(IRepository<Base_Notice> repository) : base(repository)
    {
    }
}

public class WorkShopService:CrudBaseService<Base_WorkShop>
{
    public WorkShopService(IRepository<Base_WorkShop> repository) : base(repository)
    {
    }
}