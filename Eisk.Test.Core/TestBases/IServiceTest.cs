using System;

namespace Test.Core.TestBases
{
    public interface IServiceTest<TEntity>
    {
        TEntity GetServiceInstance(Action action = null);
    }
}
