// From Steve Smith's blog (ardalis.com) :
// https://ardalis.com/double-dispatch-in-c-and-ddd/

namespace DoubleDispatch.OnlineShopping.Domain.Services
{
    using System.Linq;

    using DoubleDispatch.OnlineShopping.Domain.Model;

    // Most of the time, it's preffered to move behavior from services into
    // entities. However, sometimes behavior really belongs in a service.
    // When this occurs (and this example isn't necessarily indicative of this case),
    // you can use the same pattern we just saw with passing in a repository
    // as a parameter, but do so with a domain service. Both the aggregate root
    // (PurchaseOrder) and child (LineItem) will delegate behavior to a service
    // that's passed in as a function argument. Internally, the service will use
    // a repository when needed to get an instance of the PO.

    class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrderService(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public bool WouldAddBeUnderLimit(PurchaseOrder order, LineItem newItem)
        {
            return order.Items.Sum(i => i.Cost) + newItem.Cost <= order.SpendLimit;
        }

        public bool WouldUpdateBeUnderLimit(int purchaseOrderId, LineItem existingItem, decimal newCost)
        {
            var po = _purchaseOrderRepository.GetById(purchaseOrderId);
            // check for null, check if item belongs to PO
            return po.Items.Sum(i => i.Cost) + (newCost - existingItem.Cost) <= po.SpendLimit;
        }
    }
}
