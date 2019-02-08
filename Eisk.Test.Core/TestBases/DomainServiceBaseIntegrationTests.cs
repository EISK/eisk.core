using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Eisk.Core.DomainService;
using Xunit;

namespace Eisk.Test.Core.TestBases
{
    public abstract class DomainServiceBaseIntegrationTests<TEntity, TId> : EntityTestBase<TEntity, TId>,
        IServiceTest<DomainServiceAsync<TEntity, TId>>
        where TEntity : class, new()
    {
        private readonly DomainServiceAsync<TEntity, TId> _domainService;

        protected DomainServiceBaseIntegrationTests(DomainServiceAsync<TEntity, TId> domainService,
            Expression<Func<TEntity, TId>> idExpression) :base(idExpression)
        {
            _domainService = domainService;
        }

        public virtual DomainServiceAsync<TEntity, TId> GetServiceInstance(Action action = null)
        {
            action?.Invoke();

            return _domainService;
        }

        protected virtual async Task CreateTestEntityAsync(TEntity testEntity)
        {
            await _domainService.Add(testEntity);
        }

        [Fact]
        public virtual async Task Add_ValidDomainPassed_ShouldReturnDomainAfterCreation()
        {
            //Arrange
            var inputDomain = Factory_Entity();
            var domainService = GetServiceInstance();

            //Act
            var returnedDomain = await domainService.Add(inputDomain);

            //Assert
            Assert.NotNull(returnedDomain);
            Assert.NotEqual(default(TId), GetIdValueFromEntity(returnedDomain));
        }
        
    }
}
