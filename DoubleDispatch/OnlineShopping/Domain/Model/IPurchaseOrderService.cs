// From Steve Smith's blog (ardalis.com) :
// https://ardalis.com/double-dispatch-in-c-and-ddd/

namespace DoubleDispatch.OnlineShopping.Domain.Model
{
    interface IPurchaseOrderService
    {
        bool WouldAddBeUnderLimit(PurchaseOrder order, LineItem newItem);

        bool WouldUpdateBeUnderLimit(int purchaseOrderId, LineItem existingItem, decimal newCost);
    }
}
