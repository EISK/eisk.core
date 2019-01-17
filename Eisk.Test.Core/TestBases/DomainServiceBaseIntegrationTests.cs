using System;
using System.Linq.Expressions;
using Core.DomainService;
using Xunit;

namespace Test.Core.TestBases
{
    public abstract class DomainServiceBaseIntegrationTests<TEntity, TId> : EntityTestBase<TEntity, TId>,
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

        protected override void CreateTestEntity(TEntity testEntity)
        {
            _domainService.Add(testEntity);
        }

        [Fact]
        public virtual void Add_ValidDomainPassed_ShouldReturnDomainAfterCreation()
        {
            //Arrange
            var inputDomain = Factory_Entity();
            var domainService = Factory_Service();

            //Act
            var returnedDomain = domainService.Add(inputDomain);

            //Assert
            Assert.NotNull(returnedDomain);
            Assert.NotEqual(default(TId), GetIdValueFromEntity(returnedDomain));
        }
        
    }
}
