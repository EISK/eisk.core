using System;
using System.Linq.Expressions;
using Eisk.Core.DataService;
using Xunit;

namespace Eisk.Test.Core.TestBases
{
    public class DataServiceBaseIntegrationTests<TEntity, TId> : EntityTestBase<TEntity, TId>,
        IServiceTest<IEntityDataService<TEntity>>
        where TEntity : class, new()
    {
        private readonly IEntityDataService<TEntity> _dataService;

        protected DataServiceBaseIntegrationTests(IEntityDataService<TEntity> dataService, Expression<Func<TEntity, TId>> idExpression)
            :base(idExpression)
        {
            _dataService = dataService;
        }


        public IEntityDataService<TEntity> GetServiceInstance(Action action = null)
        {
            action?.Invoke();

            return _dataService;
        }

        protected virtual void CreateTestEntity(TEntity testEntity)
        {
            _dataService.Add(testEntity);
        }

        [Fact]
        public virtual void Add_ValidDomainPassed_ShouldReturnDomainAfterCreation()
        {
            //Arrange
            var inputEntity = Factory_Entity();
            var dataService = GetServiceInstance();

            //Act
            var returnedEntity = dataService.Add(inputEntity);

            //Assert
            Assert.NotNull(returnedEntity);
            Assert.NotEqual(default(TId), GetIdValueFromEntity(returnedEntity));
        }

        [Fact]
        public virtual void Add_ValidDomainWithRandomIdPassed_ShouldReturnDomainAfterCreation()
        {
            //Arrange
            var inputEntity = Factory_Entity();
            //might pass for sql lite, but fail for sql server
            SetIdValueToEntity(inputEntity, 100);//TODO: support for generic
            var dataService = GetServiceInstance();

            //Act
            var returnedEntity = dataService.Add(inputEntity);

            //Assert
            Assert.NotNull(returnedEntity);
            Assert.NotEqual(default(TId), GetIdValueFromEntity(returnedEntity));
        }

        [Fact]
        public virtual void Add_NullDomainPassed_ShouldThrowArgumentNullException()
        {
            //Arrange
            var dataService = GetServiceInstance();
            TEntity invalidNullDomain = null;
            
            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => dataService.Add(invalidNullDomain));

        }

        [Fact]
        public virtual void GetById_ValidIdPassed_ShouldReturnResult()
        {
            //Arrange
            var domain = Factory_Entity();
            var dataService = GetServiceInstance(() => CreateTestEntity(domain));
            var idValue = GetIdValueFromEntity(domain);
            
            //Act
            var returnedEntity = dataService.GetById(idValue);

            //Assert
            Assert.NotNull(returnedEntity);
            Assert.Equal(idValue, GetIdValueFromEntity(returnedEntity));
        }

        [Fact]
        public virtual void GetById_EmptyIdPassed_ShouldReturnNull()
        {
            //Arrange
            var dataService = GetServiceInstance();
            
            //Act
            var returnedEntity = dataService.GetById(default(TId));

            //Assert
            Assert.Null(returnedEntity);
            
        }

        [Fact]
        public virtual void GetById_InvalidIdPassed_ShouldReturnNull()
        {
            //Arrange
            var dataService = GetServiceInstance();

            //Act
            var returnedEntity = dataService.GetById(100);//TODO: make it generic random

            //Assert
            Assert.Null(returnedEntity);

        }

        [Fact]
        public virtual void Update_ValidDomainPassed_ShouldReturnDomain()
        {
            //Arrange
            var inputEntity = Factory_Entity();
            var dataService = GetServiceInstance(() =>
            {
                CreateTestEntity(inputEntity);
            });

            //Act
            var returnedEntity = dataService.Update(inputEntity);

            //Assert
            Assert.NotNull(returnedEntity);
            Assert.Equal(GetIdValueFromEntity(inputEntity), GetIdValueFromEntity(returnedEntity));

        }

        [Fact]
        public virtual void Update_ValidDomainWithEmptyIdPassed_ShouldCreateDomain()
        {
            //Arrange
            var inputEntity = Factory_Entity();
            var dataService = GetServiceInstance();

            //Act
            var returnedEntity = dataService.Update(inputEntity);//may not be supported in all data providers

            //Assert
            Assert.NotNull(returnedEntity);
            Assert.NotEqual(default(TId), GetIdValueFromEntity(returnedEntity));

        }

        [Fact]
        public virtual void Update_ValidDomainWithRandomIdPassed_ShouldThrowException()
        {
            //Arrange
            var entityWithRandomId = Factory_Entity();
            SetIdValueToEntity(entityWithRandomId, 100);//TODO: support generic
            var dataService = GetServiceInstance();

            //Act
            var ex = Record.Exception(() => dataService.Update(entityWithRandomId));

            //Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public virtual void Update_NullDomainPassed_ShouldThrowArgumentNullException()
        {
            //Arrange
            var dataService = GetServiceInstance();
            TEntity invalidNullDomain = null;

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => dataService.Update(invalidNullDomain));

        }

        [Fact]
        public virtual void Delete_DomainWithValidIdPassed_ShouldDeleteSuccessfully()
        {
            //Arrange
            var inputEntity = Factory_Entity();
            var dataService = GetServiceInstance(() => CreateTestEntity(inputEntity));
            var idValue = GetIdValueFromEntity(inputEntity);

            //Act
            dataService.Delete(inputEntity);

            //Assert
            var returnObject = dataService.GetById(idValue);
            Assert.Null(returnObject);
        }

        [Fact]
        public virtual void Delete_DomainWithEmptyIdPassed_ShouldThrowException()
        {
            //Arrange
            var inputEntity = Factory_Entity();
            var dataService = GetServiceInstance();
            
            //Act
            var returnedException = Record.Exception(() => dataService.Delete(inputEntity));

            //Assert
            Assert.NotNull(returnedException);
        }

        [Fact]
        public virtual void Delete_DomainWithRandomIdPassed_ShouldThrowException()
        {
            //Arrange
            var inputEntity = Factory_Entity();
            SetIdValueToEntity(inputEntity, 100);//TODO: support generic
            var dataService = GetServiceInstance();

            //Act
            var ex = Record.Exception(() => dataService.Delete(inputEntity));

            //Assert
            Assert.NotNull(ex);
        }
    }
}
