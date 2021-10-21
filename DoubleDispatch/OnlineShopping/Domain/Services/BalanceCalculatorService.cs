// References :

// - Jimmy Boggard's article on LosTechies's blog (lostechies.com)
// https://lostechies.com/jimmybogard/2010/03/30/strengthening-your-domain-the-double-dispatch-pattern/

namespace DoubleDispatch.OnlineShopping.Domain.Services
{
    using System.Linq;

    using DoubleDispatch.OnlineShopping.Domain.Model;

    class BalanceCalculatorService : IBalanceCalculatorService
    {
        public decimal Calculate(Fee fee)
        {
            var totalApplied = fee.Payments.Sum(payment => payment.Amount);
            return fee.Amount - totalApplied;
        }
    }
}
