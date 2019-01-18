using System;
using System.Linq.Expressions;
using Eisk.Core.DomainService;
using Xunit;

namespace Eisk.Test.Core.TestBases
{
    public abstract class DomainServiceBaseIntegrationTests<TEntity, TId> : EntityTestBase<TEntity, TId>,
        IServiceTest<DomainService<TEntity, TId>>
        where TEntity : class, new()
    {
        private readonly DomainService<TEntity, TId> _domainService;

        protected DomainServiceBaseIntegrationTests( DomainService<TEntity, TId> domainService,
            Expression<Func<TEntity, TId>> idExpression) :base(idExpression)
        {
            _domainService = domainService;
        }

        public DomainService<TEntity, TId> GetServiceInstance(Action action = null)
        {
            action?.Invoke();

            return _domainService;
        }

        protected virtual void CreateTestEntity(TEntity testEntity)
        {
            _domainService.Add(testEntity);
        }

        [Fact]
        public virtual void Add_ValidDomainPassed_ShouldReturnDomainAfterCreation()
        {
            //Arrange
            var inputDomain = Factory_Entity();
            var domainService = GetServiceInstance();

            //Act
            var returnedDomain = domainService.Add(inputDomain);

            //Assert
            Assert.NotNull(returnedDomain);
            Assert.NotEqual(default(TId), GetIdValueFromEntity(returnedDomain));
        }
        
    }
}
