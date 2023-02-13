namespace TaxesManager.Domain.Exceptions
{
    public class DuplicateException : Exception
    {
        public DuplicateException(string entityType) : base($"Provided {entityType} is a duplicate")
        {

        }
    }
}
