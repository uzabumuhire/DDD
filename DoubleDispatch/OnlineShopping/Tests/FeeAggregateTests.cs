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

            

            // To establish the relationship between Fee and Payment,
            // we have the AddPayment method, which acts as a simple
            // facade over the internal list.  
            var payment = fee.AddPayment(25m);

            // But what’s with that extra “RecalculateBalance” method?
            // In many codebases, that RecalculateBalance method would
            // be in a BalanceCalculationService, reinforcing an anemic
            // domain.
            fee.RecalculateBalance();

            // Isn’t the act of recording a payment a complete operation?
            // In the real physical world, when I give a person money, t
            // he entire transaction is completed as a whole.  Either it
            // all completes successfully, or the transaction is invalid.

            // How can I add a payment and the balance not be immediately
            // updated? It’s rather confusing to have to “remember” to use
            // these extra calculation services and helper methods, just
            // because our domain objects are too dumb to handle it
            // themselves.

            // When we called the AddPayment method, we left our Fee aggregate
            // root in an in-between state. It had a Payment, yet its balance
            // was incorrect. If Fees are supposed to act as consistency
            // boundaries, we’ve violated that consistency with this invalid
            // state.

            // This is the Inappropriate Intimacy code smell. Inappropriate
            // Intimacy is one of the biggest indicators of an anemic domain
            // model. The behavior is there, but just in the wrong place.

            Assert.Equal(25m, payment.Amount);
            Assert.Equal(75m, fee.Balance);
        }

    }
}
