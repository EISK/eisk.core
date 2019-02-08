using Eisk.Core.Exceptions;

namespace Eisk.Core.DomainService
{
    public abstract class DomainServiceBase<TDomain, TId>
        where TDomain : class, new()
    {
        protected virtual void ThrowExceptionForNullInputEntity()
        {
            throw new NullInputEntityException<TDomain>();
        }

        protected virtual void ThrowExceptionForInvalidLookupIdParameter()
        {
            throw new InvalidLookupIdParameterException<TDomain>();
        }

        protected virtual void ThrowExceptionForNonExistantEntity(TId idValue)
        {
            throw new NonExistantEntityException<TDomain>(idValue);
        }

    }
}