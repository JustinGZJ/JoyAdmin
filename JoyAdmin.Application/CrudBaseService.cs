using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Microsoft.EntityFrameworkCore;

namespace JoyAdmin.Application;

public class CrudBaseService<T>:IDynamicApiController where T :  class, IPrivateEntity, new()
{
    private readonly IRepository<T> _repository;

    public CrudBaseService(IRepository<T> repository)
    {
        _repository = repository;
    }

    // GET: api/ProcessLine
    public async Task<List<T>> GetList()
    {
        return await _repository.DetachedEntities.ToListAsync();
    }

    // GET: api/ProcessLine/5
    public async Task<T> GetOne(int id)
    {
        return await _repository.FindAsync(id);
    }

    // update
    public async Task<T> Update(T processLine)
    {
        await _repository.UpdateAsync(processLine);
        return processLine;
    }

    // delete
    public async Task<T> Delete(int id)
    {
        var entity = await _repository.FindAsync(id);
        await _repository.DeleteAsync(entity);
        return entity;
    }

    // add
    public async Task<T> Add(T processLine)
    {
        await _repository.InsertAsync(processLine);
        return processLine;
    }
}