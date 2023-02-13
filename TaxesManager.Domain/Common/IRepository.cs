namespace TaxesManager.Domain.Common
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
