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

        // It could be argued that the Fee shouldn’t be responsible for how the balance
        // is calculated, but instead only ensure that when a payment is recorded,
        // the balance is updated.

        // The problem comes in when calculating the balance becomes more difficult.
        // We might have a rather complex method for calculating payments, we might
        // have recurring payments, transfers, debits, credits and so on.  This might
        // become too much responsibility for the Fee object.

        // Store a calculated balance for performance and querying abilities reasons.
        private void RecalculateBalance()
        {
            var totalApplied = _payments.Sum(payment => payment.Amount);
            Balance = Amount - totalApplied;
        }
    }
}
