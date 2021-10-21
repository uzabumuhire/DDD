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
        public Payment RecordPayment(decimal paymentAmount)
        {
            var payment = new Payment(paymentAmount, this);
            _payments.Add(payment);

            // Enforce the aggregate boundaries with encapsulation, that is encapsulating
            // the operation of recording a fee.  Even the name of “AddPayment” could be
            // improved, to “RecordPayment”.  The act of recording a payment in the Real
            // World involves adding the payment to the ledger and updating the balance
            // book.  If the accountant solely adds the payment to the ledger, but does
            // not update the balance book, they haven’t yet finished recording the payment.
            RecalculateBalance();

            return payment;
        }

        // Store a calculated balance for performance and querying abilities reasons.
        private void RecalculateBalance()
        {
            // Made the Recalculate method private, as this is an implementation detail
            // of how the Fee object keeps the balance consistent. From someone using
            // the Fee object, we don’t care how the Fee object keeps itself consistent,
            // we only want to care that it is consistent.

            // The public contour of the Fee object is simplified as well. We only expose
            // operations that we want to support, captured in the names of our ubiquitous
            // language. The “How” is encapsulated behind the aggregate root boundary.
            var totalApplied = _payments.Sum(payment => payment.Amount);
            Balance = Amount - totalApplied;
        }
    }
}
