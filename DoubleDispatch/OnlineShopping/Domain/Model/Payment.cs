// References :

// - Jimmy Boggard's article on LosTechies's blog (lostechies.com)
// https://lostechies.com/jimmybogard/2010/03/24/strengthening-your-domain-encapsulating-operations/

namespace DoubleDispatch.OnlineShopping.Domain.Model
{
    class Payment
    {
        public Payment(decimal amount, Fee fee)
        {
            Amount = amount;
        }

        public decimal Amount { get; }
    }
}
