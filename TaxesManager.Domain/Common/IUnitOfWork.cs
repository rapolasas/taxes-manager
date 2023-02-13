namespace TaxesManager.Domain.Common
{
    public interface IUnitOfWork
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken);
    }
}
