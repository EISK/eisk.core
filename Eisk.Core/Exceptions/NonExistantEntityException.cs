namespace Eisk.Core.Exceptions
{
    public class NonExistantEntityException<T>: CoreException
        
    {
        public NonExistantEntityException(object paramValue, string paramName = "id") : base(
            $"No {typeof(T).Name} exists for given id {paramValue} for parameter {paramName}.", "APP-DATA-ERROR-001")
        {
            
        }
    }
}