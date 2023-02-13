using TaxesManager.Domain.Common;
using TaxesManager.Domain.Exceptions;
using TaxesManager.Domain.Taxes;

namespace TaxesManager.Domain.Municipalities
{
    public class Municipality : Entity
    {
        public string Name { get; set; }

        private readonly List<Tax> _taxes = new();
        public IReadOnlyList<Tax> Taxes => _taxes.AsReadOnly();

        public Municipality(string name)
        {
            Name = name;
        }

        public Tax AddTax(decimal amount, DateTime startDate, TaxSchedule schedule)
        {
            if(_taxes.Any(x => x.Exists(startDate, schedule)))
            {
                throw new DuplicateException(nameof(Tax));
            }

            var tax = new Tax(amount, startDate, schedule);
            _taxes.Add(tax);

            return tax;
        }

        public Tax FindTax(DateTime date)
        {
            var tax = _taxes
                .Where(tax => tax.StartDate <= date && tax.EndDate >= date)
                .OrderByDescending(tax => tax.Schedule)
                .FirstOrDefault();

            if(tax is null)
            {
                throw new NotFoundException(nameof(Tax));
            }

            return tax;
        }
    }
}
