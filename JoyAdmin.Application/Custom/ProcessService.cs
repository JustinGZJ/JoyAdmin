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

public class ProcessService:IDynamicApiController
{
    private readonly IRepository<Base_Process> _processRepository;

    public ProcessService(IRepository<Base_Process> processRepository)
    {
        _processRepository = processRepository;
    }
    // GET: api/Process
    
    public async Task<List<Base_Process>> GetProcessList()
    {
        return await _processRepository.DetachedEntities.ToListAsync();
    }
    
    // GET: api/Process/5
    
    public async Task<Base_Process> GetProcess(int id)
    {
        return await _processRepository.DetachedEntities.FirstOrDefaultAsync(a => a.Process_Id == id);
    }
    
    // update
    
    public async Task<Base_Process> UpdateProcess(Base_Process process)
    {
        var entity = await _processRepository.DetachedEntities.FirstOrDefaultAsync(a => a.Process_Id == process.Process_Id);
        // 使用mapper赋值
        entity = process.Adapt(entity);
        await _processRepository.UpdateAsync(entity);
        return entity;
    }
    
    // add
    
    public async Task<Base_Process> AddProcess(Base_Process process)
    {
        await _processRepository.InsertAsync(process);
        return process;
    }
    
    // delete
    
    public async Task<Base_Process> DeleteProcess(int id)
    {
        var entity = await _processRepository.DetachedEntities.FirstOrDefaultAsync(a => a.Process_Id == id);
        await _processRepository.DeleteAsync(entity);
        return entity;
    }
    

    
}