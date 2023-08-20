using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace JoyAdmin.Application.Custom;

public class ProcessLineListService
{
    private readonly IRepository<Base_ProcessLineList> _repository;

    public ProcessLineListService(IRepository<Base_ProcessLineList> repository)
    {
        _repository = repository;
    }
    
    // GET: api/ProcessLineList
    
    public async Task<List<Base_ProcessLineList>> GetProcessLineListList()
    {
        return await _repository.DetachedEntities.ToListAsync();
    }
    
    // GET: api/ProcessLineList/5
    
    public async Task<Base_ProcessLineList> GetProcessLineList(int id)
    {
        return await _repository.DetachedEntities.FirstOrDefaultAsync(a => a.ProcessLineList_Id == id);
    }
    
    // update
    
    public async Task<Base_ProcessLineList> UpdateProcessLineList(Base_ProcessLineList processLineList)
    {
        var entity = await _repository.DetachedEntities.FirstOrDefaultAsync(a => a.ProcessLineList_Id == processLineList.ProcessLineList_Id);
        // 使用mapper赋值
        entity = processLineList.Adapt(entity);
        await _repository.UpdateAsync(entity);
        return entity;
    }
    
    // delete
    
    public async Task<Base_ProcessLineList> DeleteProcessLineList(int id)
    {
        var entity = await _repository.DetachedEntities.FirstOrDefaultAsync(a => a.ProcessLineList_Id == id);
        await _repository.DeleteAsync(entity);
        return entity;
    }
    
    // add
    
    public async Task<Base_ProcessLineList> AddProcessLineList(Base_ProcessLineList processLineList)
    {
        await _repository.InsertAsync(processLineList);
        return processLineList;
    }
}