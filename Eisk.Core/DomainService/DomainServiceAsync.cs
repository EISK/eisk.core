using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eisk.Core.DataService;
using Eisk.Core.Exceptions;
using Eisk.Core.Utils;

namespace Eisk.Core.DomainService
{
    public class DomainServiceAsync<TDomain, TId> : DomainServiceBase<TDomain, TId>
        where TDomain : class, new()
    {
        public readonly IEntityDataServiceAsync<TDomain> EntityDataService;

        public DomainServiceAsync(IEntityDataServiceAsync<TDomain> entityDataService)
        {
            EntityDataService = entityDataService;
        }

        public virtual async Task<IEnumerable<TDomain>> GetAll()
        {
            return await EntityDataService.GetAll();
        }

        public virtual async Task<TDomain> GetById(TId id)
        {
            if (id.IsNullOrEmpty())
                ThrowExceptionForInvalidLookupIdParameter();

            var entityInDb = await EntityDataService.GetById(id);

            if (entityInDb == null)
                ThrowExceptionForNonExistantEntity(id);

            return entityInDb;
        }

        public virtual async Task<TDomain> Add(TDomain entity)
        {
            return await Add(entity, null);
        }

        public virtual async Task<TDomain> Add(TDomain entity, Action<TDomain> preProcessAction, Action<TDomain> postProcessAction = null)
        {
            if (entity == null)
                ThrowExceptionForNullInputEntity();

            preProcessAction?.Invoke(entity);

            var returnVal = await EntityDataService.Add(entity);

            postProcessAction?.Invoke(returnVal);

            return returnVal;
        }

        public virtual async Task<TDomain> Update(TId id, TDomain newEntity)
        {
            return await Update(id, newEntity, null);
        }

        public virtual async Task<TDomain> Update(TId id, TDomain newEntity, Action<TDomain, TDomain> preProcessAction, Action<TDomain> postProcessAction = null)
        {
            if (id.IsNullOrEmpty())
                ThrowExceptionForInvalidLookupIdParameter();

            if (newEntity == null)
                ThrowExceptionForNullInputEntity();

            var oldEntity = await GetById(id);

            preProcessAction?.Invoke(oldEntity, newEntity);

            var returnVal = await EntityDataService.Update(newEntity);

            postProcessAction?.Invoke(returnVal);

            return returnVal;
        }

        public virtual async Task Delete(TId id)
        {
            if (id.IsNullOrEmpty())
                ThrowExceptionForInvalidLookupIdParameter();

            var entityInDb = await GetById(id);

            await EntityDataService.Delete(entityInDb);
        }
    }
}