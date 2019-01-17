using System;
using System.Linq.Expressions;
using Core.DomainService;
using Xunit;

namespace Test.Core.TestBases
{
    public abstract class DomainServiceBaseIntegrationTests<TEntity, TId> : EntityServiceTestBase<TEntity, TId>,
        IServiceTest<DomainService<TEntity,TId>>
        where TEntity : class, new()
    {
        private readonly DomainService<TEntity, TId> _domainService;

        protected DomainServiceBaseIntegrationTests(Expression<Func<TEntity, TId>> idExpression, 
            DomainService<TEntity, TId> domainService):base(idExpression)
        {
            _domainService = domainService;
        }

        public DomainService<TEntity, TId> Factory_Service(Action action = null)
        {
            action?.Invoke();

            return _domainService;
        }

        protected void CreateARecord(TEntity getEntity)
        {
            _domainService.Add(getEntity);
        }

        [Fact]
        public virtual void Add_ValidDomainPassed_ShouldReturnDomainAfterCreation()
        {
            //Arrange
            var domainInput = Factory_Entity();
            SetIdValueToEntity(domainInput, default(TId));//TODO: support for non-auto Id's

            var service = Factory_Service();

            //Act
            var domainReturned = service.Add(domainInput);

            //Assert
            Assert.NotNull(domainReturned);
            Assert.NotEqual(default(TId), GetIdValueFromEntity(domainReturned));
        }
    }
}
