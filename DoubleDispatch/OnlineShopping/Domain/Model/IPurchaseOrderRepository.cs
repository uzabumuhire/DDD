namespace DoubleDispatch.OnlineShopping.Domain.Model
{
    interface IPurchaseOrderRepository
    {
        PurchaseOrder GetById(int id);
    }
}
