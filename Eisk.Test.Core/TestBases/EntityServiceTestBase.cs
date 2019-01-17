using System;
using System.Linq.Expressions;
using Core.DataService;
using Core.Utils;

namespace Test.Core.TestBases
{
    public abstract class EntityServiceTestBase<TEntity, TId> : EntityTestBase<TEntity>
        where TEntity : class, new()
    {
        protected readonly Expression<Func<TEntity, TId>> _idExpression;
        
        protected EntityServiceTestBase(Expression<Func<TEntity, TId>> idExpression)
        {
            _idExpression = idExpression;
        }

        protected TId GetIdValueFromEntity(TEntity entity)
        {
            return (TId)ExpressionUtil<TEntity>.GetPropertyValue(_idExpression, entity);
        }

        protected void SetIdValueToEntity(TEntity entity, object value)
        {
            ExpressionUtil<TEntity>.SetPropertyValue(_idExpression, entity, value);
        }
    }
}
