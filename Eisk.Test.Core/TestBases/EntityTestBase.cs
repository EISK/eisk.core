using System;
using System.Linq.Expressions;
using Core.Utils;

namespace Test.Core.TestBases
{
    public abstract class EntityTestBase<TEntity, TId> : TestBase
        where TEntity : class, new()
    {
        protected readonly Expression<Func<TEntity, TId>> _idExpression;
        
        protected EntityTestBase(Expression<Func<TEntity, TId>> idExpression)
        {
            _idExpression = idExpression;
        }

        protected virtual TEntity Factory_Entity(Action<TEntity> action = null, bool setIdWithDefault = true)
        {
            var entity = Factory_Entity<TEntity>();

            if (setIdWithDefault)
                SetIdValueToEntity(entity, default(TId));

            action?.Invoke(entity);
            
            return entity;
        }

        protected TId GetIdValueFromEntity(TEntity entity)
        {
            return (TId)ExpressionUtil<TEntity>.GetPropertyValue(_idExpression, entity);
        }

        protected void SetIdValueToEntity(TEntity entity, object value)
        {
            ExpressionUtil<TEntity>.SetPropertyValue(_idExpression, entity, value);
        }

        protected abstract void CreateTestEntity(TEntity testEntity);

    }
}
