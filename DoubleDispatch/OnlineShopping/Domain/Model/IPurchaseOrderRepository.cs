// From Steve Smith's blog (ardalis.com) :
// https://ardalis.com/double-dispatch-in-c-and-ddd/

namespace DoubleDispatch.OnlineShopping.Domain.Model
{
    interface IPurchaseOrderRepository
    {
        PurchaseOrder GetById(int id);
    }
}
