// References :

// - Jimmy Boggard's articles on LosTechies's blog (lostechies.com)
// https://lostechies.com/jimmybogard/2010/03/24/strengthening-your-domain-encapsulating-operations/
// https://lostechies.com/jimmybogard/2010/02/24/strengthening-your-domain-aggregate-construction/
// https://lostechies.com/jimmybogard/2010/03/30/strengthening-your-domain-the-double-dispatch-pattern/

namespace DoubleDispatch.OnlineShopping.Tests
{
    using Xunit;

    using DoubleDispatch.OnlineShopping.Domain.Model;
    using DoubleDispatch.OnlineShopping.Domain.Services;

    public class FeeAggregateTests
    {
        [Fact]
        public void Record_a_payment_against_a_fee()
        {
            var customer = new Customer();
            var fee = customer.ChargeFee(100m);
            var balanceCalculatorService = new BalanceCalculatorService();
            var payment = fee.RecordPayment(25m, balanceCalculatorService);

            Assert.Equal(25m, payment.Amount);
            Assert.Equal(75m, fee.Balance);
        }

    }
}
