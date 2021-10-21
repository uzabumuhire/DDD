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

            var payment = fee.AddPayment(25m);
            fee.RecalculateBalance();

            Assert.Equal(25m, payment.Amount);
            Assert.Equal(75m, fee.Balance);
        }

    }
}
