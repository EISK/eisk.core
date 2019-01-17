using System;
using Core.DataService;
using Microsoft.EntityFrameworkCore;

namespace Test.Core.DataGen
{
    public class EntityTestDataService<TEntity> 
        where TEntity : class, new()
    {
        EntityContextDataService<TEntity> _entityContextDataService;
        public EntityTestDataService(DbContext dbContext)
        {
            _entityContextDataService = new EntityContextDataService<TEntity>(dbContext);
        }

        public virtual TEntity Add_TestData_InStore(Action<TEntity> action = null)
        {
            var entity = DomainDataFactory<TEntity>.Create_Entity(action);
            return _entityContextDataService.Add(entity);
        }
    }
}
