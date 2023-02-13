namespace TaxesManager.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityType) : base($"Specified {entityType} was not found")
        {

        }
    }
}
