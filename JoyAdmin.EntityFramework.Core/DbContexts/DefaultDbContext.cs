using JoyAdmin.Core;
using Furion;
using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Linq.Expressions;
using Furion.FriendlyException;
using Yitter.IdGenerator;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JoyAdmin.EntityFramework.Core;
//  [AppDbContext("SqlServerConnectionString")]

[AppDbContext("PgSqlConnectionString")]
public class DefaultDbContext : AppDbContext<DefaultDbContext>, IModelBuilderFilter

{
    public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
    {
        EnabledEntityStateTracked = false;
        // 忽略空值更新
        InsertOrUpdateIgnoreNullValues = true;

        var op = new IdGeneratorOptions(0);
        YitIdHelper.SetIdGenerator(op);
    }

    public void OnCreating(ModelBuilder modelBuilder, EntityTypeBuilder entityBuilder, DbContext dbContext,
        Type dbContextLocator)
    {
        //假删除过滤器
        LambdaExpression expression = FakeDeleteQueryFilterExpression(entityBuilder, dbContext);
        if (expression != null)
            entityBuilder.HasQueryFilter(expression);
    }

    protected override void SavingChangesEvent(DbContextEventData eventData, InterceptionResult<int> result)
    {
        // 获取当前事件对应上下文
        var dbContext = eventData.Context;

        // 获取所有新增和更新的实体
        var entities = dbContext.ChangeTracker.Entries().Where(u =>
            u.State == EntityState.Added || u.State == EntityState.Modified || u.State == EntityState.Deleted);
        if(App.User==null)
            return;
        var userId = App.User.FindFirst(ClaimConst.CLAINM_USERID)?.Value;
        var userName = App.User.FindFirst(ClaimConst.CLAINM_ACCOUNT)?.Value;
        foreach (var entity in entities)
        {
            if (!entity.Entity.GetType().IsSubclassOf(typeof(BaseEntity))) continue;
            var obj = entity.Entity as BaseEntity;
            if (entity.State == EntityState.Added)
            {
                obj.Id = obj.Id == 0 ? YitIdHelper.NextId() : obj.Id;
                obj.CreatedTime = DateTime.Now;
                if (string.IsNullOrEmpty(userId)) continue;
                obj.CreatedUserId = long.Parse(userId);
                obj.CreatedUser = userName;
            }
            else if (entity.State == EntityState.Modified)
            {
                obj.ModifiedTime = DateTime.Now;
                if (!string.IsNullOrEmpty(userId))
                {
                    obj.ModifiedUserId = long.Parse(userId);
                    obj.ModifiedUser = userName;
                }

                entity.Property(nameof(BaseEntity.CreatedTime)).IsModified = false;
                entity.Property(nameof(BaseEntity.CreatedUserId)).IsModified = false;
                entity.Property(nameof(BaseEntity.CreatedUser)).IsModified = false;
            }
        }
    }

    /// <summary>
    /// 假删除过滤器
    /// </summary>
    /// <param name="entityBuilder"></param>
    /// <param name="dbContext"></param>
    /// <param name="isDeletedKey"></param>
    /// <param name="filterValue"></param>
    /// <returns></returns>
    protected static LambdaExpression FakeDeleteQueryFilterExpression(EntityTypeBuilder entityBuilder,
        DbContext dbContext, string isDeletedKey = null, object filterValue = null)
    {
        isDeletedKey ??= "IsDeleted";
        IMutableEntityType metadata = entityBuilder.Metadata;
        if (metadata.FindProperty(isDeletedKey) == null)
        {
            return null;
        }

        Expression finialExpression = Expression.Constant(true);
        ParameterExpression parameterExpression = Expression.Parameter(metadata.ClrType, "u");
        // 假删除过滤器
        if (metadata.FindProperty(isDeletedKey) == null)
            return Expression.Lambda(finialExpression, parameterExpression);
        ConstantExpression constantExpression = Expression.Constant(isDeletedKey);
        ConstantExpression right = Expression.Constant(filterValue ?? false);
        var fakeDeleteQueryExpression = Expression.Equal(Expression.Call(typeof(EF), "Property", new Type[1]
        {
            typeof(bool)
        }, parameterExpression, constantExpression), right);
        finialExpression = Expression.AndAlso(finialExpression, fakeDeleteQueryExpression);

        return Expression.Lambda(finialExpression, parameterExpression);
    }
}