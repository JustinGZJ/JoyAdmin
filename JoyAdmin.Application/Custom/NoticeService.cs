using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class NoticeService:CrudBaseService<Base_Notice>
{
    public NoticeService(IRepository<Base_Notice> repository) : base(repository)
    {
    }
}