using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using JoyAdmin.Application.Dto;
using Microsoft.EntityFrameworkCore;

namespace JoyAdmin.Application;

public class ServiceBase<T> : IDynamicApiController where T : class, IPrivateEntity, new()
{
    private readonly IRepository<T> _workorderRepository;


    public ServiceBase(IRepository<T> workorderRepository)
    {
        _workorderRepository = workorderRepository;
    }

    public async Task<PagedList<T>> FilterList(FilterDto filter)
    {
        var query = _workorderRepository.DetachedEntities.AsQueryable();

        if (filter.filterProperty != null)
        {
            var condition = $"{filter.filterProperty} == @0";
            query = query.Where(condition, filter.filterValue.ToString());
        }

        if (filter.sortProperty == null) return await query.ToPagedListAsync(filter.page, filter.size);
        var sortExpression = filter.sortProperty;
        if (filter.desc) sortExpression += " DESC";
        query = query.OrderBy(sortExpression);

        return await query.ToPagedListAsync(filter.page, filter.size);
    }
    
    public async Task<PagedList<T>> DynamicFilter(DynamicFilterDto filter)
    {
        var query = _workorderRepository.DetachedEntities.AsQueryable();

        if (filter.filterExp != null)
        {
            query = query.Where(filter.filterExp);
        }
        if(filter.sortExp!=null)
            query = query.OrderBy(filter.sortExp);
        return await query.ToPagedListAsync(filter.page, filter.size);
    }

    // GET: api/ProcessLine
    public async Task<List<T>> GetList()
    {
        return await _workorderRepository.DetachedEntities.ToListAsync();
    }

    // GET: api/ProcessLine/5
    public async Task<T> GetOne(int id)
    {
        return await _workorderRepository.FindAsync(id);
    }


    // delete
    public async Task<T> Delete(int id)
    {
        var entity = await _workorderRepository.FindAsync(id);
        await _workorderRepository.DeleteNowAsync(entity);
        return entity;
    }

    // add
    public async Task<T> Add(T entity)
    {
        await _workorderRepository.InsertNowAsync(entity);
        return entity;
    }

    // Batch Add
    public async Task<IEnumerable<T>> BatchAdd(T[] entities)
    {
        await _workorderRepository.InsertNowAsync(entities);
        return entities;
    }


    // update
    public async Task<T> Update(T entity)
    {
        await _workorderRepository.UpdateAsync(entity);
        return entity;
    }

    // Batch update
    public async Task<IEnumerable<T>> BatchUpdate(T[] entities)
    {
        await _workorderRepository.UpdateAsync(entities);
        return entities;
    }
}