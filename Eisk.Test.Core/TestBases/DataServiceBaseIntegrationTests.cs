﻿using System;
using System.Linq.Expressions;
using Core.DataService;
using Core.Utils;
using Xunit;

namespace Test.Core.TestBases
{
    public class DataServiceBaseIntegrationTests<TEntity, TId> : EntityTestBase<TEntity>, 
        IServiceTest<IEntityDataService<TEntity>>
        where TEntity : class, new()
    {
        private readonly IEntityDataService<TEntity> _dataService;

        private readonly Expression<Func<TEntity, TId>> _idExpression;

        protected DataServiceBaseIntegrationTests(IEntityDataService<TEntity> dataService, Expression<Func<TEntity, TId>> idExpression)
        {
            _dataService = dataService;
            _idExpression = idExpression;
        }


        public IEntityDataService<TEntity> Factory_Service(Action action = null)
        {
            action?.Invoke();

            return _dataService;
        }

        protected TId GetIdValueFromEntity(TEntity entity)
        {
            return (TId)ExpressionUtil<TEntity>.GetPropertyValue(_idExpression, entity);
        }

        private void SetIdValueToEntity(TEntity entity, object value)
        {
            ExpressionUtil<TEntity>.SetPropertyValue(_idExpression, entity, value);
        }

        protected void SetupGetById(TEntity getEntity)
        {
            _dataService.Add(getEntity);
        }

        [Fact]
        public virtual void Add_ValidDomainPassed_ShouldReturnDomainAfterCreation()
        {
            //Arrange
            var domainInput = Factory_Entity();
            SetIdValueToEntity(domainInput, default(TId));

            var service = Factory_Service();

            //Act
            var domainReturned = service.Add(domainInput);

            //Assert
            Assert.NotNull(domainReturned);
            Assert.NotEqual(default(TId), GetIdValueFromEntity(domainReturned));
        }

        [Fact]
        public virtual void Add_NullDomainPassed_ShouldThrowArgumentNullException()
        {
            //Arrange
            var service = Factory_Service();


            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => service.Add(null));

        }

        [Fact]
        public virtual void GetById_ValidIdPassed_ShouldReturnResult()
        {
            //Arrange
            var domain = Factory_Entity();
            var service = Factory_Service(() => SetupGetById(domain));
            var idValue = GetIdValueFromEntity(domain);
            
            //Act
            var domainReturned = service.GetById(idValue);

            //Assert
            Assert.NotNull(domainReturned);
            Assert.Equal(idValue, GetIdValueFromEntity(domainReturned));
        }

        [Fact]
        public virtual void GetById_InvalidIdPassed_ShouldReturnNull()
        {
            //Arrange
            var domain = Factory_Entity();
            var service = Factory_Service();
            
            //Act
            var domainReturned = service.GetById(default(TId));

            //Assert
            Assert.Null(domainReturned);
            
        }

        [Fact]
        public virtual void Update_ValidDomainPassed_ShouldReturnDomain()
        {
            //Arrange
            var domainInput = Factory_Entity();
            var service = Factory_Service(() =>
            {
                SetupGetById(domainInput);
            });

            //Act
            var domainReturned = service.Update(domainInput);

            //Assert
            Assert.NotNull(domainReturned);
            Assert.Equal(GetIdValueFromEntity(domainInput), GetIdValueFromEntity(domainReturned));

        }

        [Fact]
        public virtual void Update_ValidDomainPassed_()
        {
            //Arrange
            var domainInput = Factory_Entity();
            //SetIdValueToEntity(domainInput, default(TId));
            var service = Factory_Service();

            //Act
            var domainReturned = service.Update(domainInput);
            

        }

    }
}
