// References :

// - Jimmy Boggard's article on LosTechies's blog (lostechies.com)
// https://lostechies.com/jimmybogard/2010/03/24/strengthening-your-domain-encapsulating-operations/

namespace DoubleDispatch.OnlineShopping.Tests
{  
    using Xunit;

    using DoubleDispatch.OnlineShopping.Domain.Model;

    public class CustomerAggregateTests
    {
        [Fact]
        public void Apply_the_fee_to_the_customer_when_charging_a_customer_a_fee()
        {
            var customer = new Customer();

            var fee = customer.ChargeFee(100m);

            Assert.Equal(100m, fee.Amount);
            Assert.Contains(customer.Fees, f => f == fee);
        }
    }
}
