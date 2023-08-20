using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace JoyAdmin.Application.Custom;

public class ProcessLIneService
{
    private readonly IRepository<Base_ProcessLine> _repository;

    public ProcessLIneService(IRepository<Base_ProcessLine> repository)
    {
        _repository = repository;
    }
    
    // GET: api/ProcessLine
    
    public async Task<List<Base_ProcessLine>> GetProcessLineList()
    {
        return await _repository.DetachedEntities.ToListAsync();
    }
    
    // GET: api/ProcessLine/5
    
    public async Task<Base_ProcessLine> GetProcessLine(int id)
    {
        return await _repository.DetachedEntities.FirstOrDefaultAsync(a => a.ProcessLine_Id == id);
    }
    
    // update
    
    public async Task<Base_ProcessLine> UpdateProcessLine(Base_ProcessLine processLine)
    {
        var entity = await _repository.DetachedEntities.FirstOrDefaultAsync(a => a.ProcessLine_Id == processLine.ProcessLine_Id);
        // 使用mapper赋值
        entity = processLine.Adapt(entity);
        await _repository.UpdateAsync(entity);
        return entity;
    }
    
    // delete
    
    public async Task<Base_ProcessLine> DeleteProcessLine(int id)
    {
        var entity = await _repository.DetachedEntities.FirstOrDefaultAsync(a => a.ProcessLine_Id == id);
        await _repository.DeleteAsync(entity);
        return entity;
    }
    
    // add
    
    public async Task<Base_ProcessLine> AddProcessLine(Base_ProcessLine processLine)
    {
        await _repository.InsertAsync(processLine);
        return processLine;
    }
}