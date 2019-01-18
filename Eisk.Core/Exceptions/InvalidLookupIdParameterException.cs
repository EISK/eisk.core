namespace Eisk.Core.Exceptions
{
    public class InvalidLookupIdParameterException<TEntity>: CoreException
    {
        public InvalidLookupIdParameterException(string paramName = "id") : base(
            $"Invalid lookup parameter: {paramName} to find {typeof(TEntity).Name}.", "APP-DATA-ERROR-001")
        {

        }
    }
}