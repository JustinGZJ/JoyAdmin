using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using JoyAdmin.Core.Entities.Custom;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JoyAdmin.Application.Custom;


public class ProcessService : ServiceBase<Base_Process>
{
    public ProcessService(IRepository<Base_Process> repository) : base(repository)
    {
    }
}