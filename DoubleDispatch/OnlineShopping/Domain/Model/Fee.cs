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

        // Determines the Fee’s balance.
        public decimal Balance { get; private set; }

        // A Customer can make Payments against a charged Fees.
        public Payment AddPayment(decimal paymentAmount)
        {
            var payment = new Payment(paymentAmount, this);
            _payments.Add(payment);
            return payment;
        }

        // Store a calculated balance for performance and querying abilities reasons.
        public void RecalculateBalance()
        {
            var totalApplied = _payments.Sum(payment => payment.Amount);
            Balance = Amount - totalApplied;
        }
    }
}
