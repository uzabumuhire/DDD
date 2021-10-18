// From Steve Smith's blog (ardalis.com) :
// https://ardalis.com/double-dispatch-in-c-and-ddd/

namespace DoubleDispatch.OnlineShopping.Domain.Model
{
    interface IPurchaseOrderRepository
    {
        void Add(PurchaseOrder purchaseOrder);

        PurchaseOrder GetById(int id);
    }
}
