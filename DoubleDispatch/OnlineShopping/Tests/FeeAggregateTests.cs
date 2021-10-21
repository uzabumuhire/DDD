// References :

// - Jimmy Boggard's article on LosTechies's blog (lostechies.com)
// https://lostechies.com/jimmybogard/2010/03/24/strengthening-your-domain-encapsulating-operations/

namespace DoubleDispatch.OnlineShopping.Tests
{
    using Xunit;

    using DoubleDispatch.OnlineShopping.Domain.Model;

    public class FeeAggregateTests
    {
        [Fact]
        public void Record_a_payment_against_a_fee()
        {
            var customer = new Customer();

            var fee = customer.ChargeFee(100m);

            // We got rid of the extra “Recalculate” call, and now
            // encapsulated the entire command of “Record this payment”
            // to our aggregate root, the Fee object. The RecordPayment
            // method now encapsulates the complete operation of recording
            // a payment, ensuring that the Fee root is self-consistent
            // at the completion of the operation.
            var payment = fee.RecordPayment(25m);

            Assert.Equal(25m, payment.Amount);
            Assert.Equal(75m, fee.Balance);
        }

    }
}
