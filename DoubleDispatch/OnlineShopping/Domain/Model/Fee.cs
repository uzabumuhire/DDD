// References :

// - Jimmy Boggard's article on LosTechies's blog (lostechies.com)
// https://lostechies.com/jimmybogard/2010/03/24/strengthening-your-domain-encapsulating-operations/https://lostechies.com/jimmybogard/2010/02/24/strengthening-your-domain-aggregate-construction/

namespace DoubleDispatch.OnlineShopping.Domain.Model
{
    using System.Linq;
    using System.Collections.Generic;

    class Fee
    {
        private List<Payment> _payments = new();
        public Fee(decimal amount, Customer customer)
        {
            Amount = amount;
        }

        public decimal Amount { get; }

        public decimal Balance { get; private set; }

        public Payment AddPayment(decimal paymentAmount)
        {
            var payment = new Payment(paymentAmount, this);
            _payments.Add(payment);
            return payment;
        }

        public void RecalculateBalance()
        {
            var totalApplied = _payments.Sum(payment => payment.Amount);
            Balance = Amount - totalApplied;
        }
    }
}
