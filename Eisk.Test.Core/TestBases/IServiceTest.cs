using System;

namespace Test.Core.TestBases
{
    public interface IServiceTest<TEntity>
    {
        TEntity Factory_Service(Action action = null);
    }
}
