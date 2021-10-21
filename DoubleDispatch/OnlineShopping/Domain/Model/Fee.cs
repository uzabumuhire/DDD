// References :

// - Jimmy Boggard's articles on LosTechies's blog (lostechies.com)
// https://lostechies.com/jimmybogard/2010/03/24/strengthening-your-domain-encapsulating-operations/
// https://lostechies.com/jimmybogard/2010/03/30/strengthening-your-domain-the-double-dispatch-pattern/

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

        public IEnumerable<Payment> Payments => _payments;

        // When a domain object begins to contain too many responsibilities,
        // we start to break out those extra responsibilities into things like
        // value objects and domain services. This does not mean we have to
        // give up consistency and closure of operations, however. With the use
        // of the double dispatch pattern, we can avoid anemic domain models,
        // as well as the attempt to inject services into our domain model.
        // Our methods stay very intention-revealing, showing exactly what is
        // needed to fulfill a request of recording a payment.

        // A Customer can make Payments against a charged Fees.
        public Payment RecordPayment(
            decimal paymentAmount, 
            IBalanceCalculatorService balanceCalculatorService)
        {
            var payment = new Payment(paymentAmount, this);
            _payments.Add(payment);

            // Use a BalanceCalculator service to ensure that when a
            // payment is recorded, the balance is updated.

            // The double dispatch pattern involves passing an object to a method,
            // and the method body calls another method on the passed in object,
            // usually passing in itself as an argument.

            // The intent of the RecordPayment method remains the same: it records
            // a payment, and updates the balance. The balance on the Fee object
            // will always be correct. The RecordPayment method now delegates to
            // a domain service, the IBalanceCalculatorService, for calculation of
            // the balance. However, the Fee object is still responsible for
            // maintaining a correct balance. We just call the Calculate method on
            // the balance calculator, passing in “this”, to figure out what the
            // actual correct balance can be.
            Balance = balanceCalculatorService.Calculate(this);

            return payment;
        }
    }
}
