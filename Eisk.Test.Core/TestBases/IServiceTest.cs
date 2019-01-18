using System;

namespace Eisk.Test.Core.TestBases
{
    public interface IServiceTest<out TEntity>
    {
        TEntity GetServiceInstance(Action action = null);
    }
}
