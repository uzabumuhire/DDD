// References :

// - Jimmy Boggard's article on LosTechies's blog (lostechies.com)
// https://lostechies.com/jimmybogard/2010/03/30/strengthening-your-domain-the-double-dispatch-pattern/

namespace DoubleDispatch.OnlineShopping.Domain.Model
{
    // Represents our balance calculator domain service.
    interface IBalanceCalculatorService
    {
        // Calculate the balance for the Fee. It doesn’t try to modify the Fee,
        // allowing for a side-effect free function. I can call the calculator
        // as many times as I like with a given Fee, and I can be assured that
        // the Fee object won’t be changed.
        decimal Calculate(Fee fee);
    }
}
