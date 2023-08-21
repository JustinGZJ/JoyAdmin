using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using JoyAdmin.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JoyAdmin.Application;

public class CrudBaseService<T> : IDynamicApiController where T : class, IPrivateEntity, new()
{
    private readonly IRepository<T> _repository;


    public CrudBaseService(IRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<PagedList<T>> FilterList(FilterDto filter)
    {
        var query = _repository.DetachedEntities.AsQueryable();

        if (filter.filterProperty != null)
        {
            string condition = $"{filter.filterProperty} == @0";
            query = query.Where(condition, filter.filterValue);
        }

        if (filter.sortProperty == null) return await query.ToPagedListAsync(filter.page, filter.size);
        string sortExpression = filter.sortProperty;
        if (filter.desc)
        {
            sortExpression += " DESC";
        }
        query = query.OrderBy(sortExpression);

        return await query.ToPagedListAsync(filter.page, filter.size);
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