
using ElecronicsStore.DB.Models;
using ElecronicsStore.DB.Storages;
using System;
using System.Threading.Tasks;

namespace ElectronicsStore.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private IOrderStorage _orderStorage;

        public OrderRepository(IOrderStorage orderStorage)
        {
            _orderStorage = orderStorage;
        }

        public async ValueTask<RequestResult<long>> AddOrUpdateOrder(Order dataModel)
        {
            var result = new RequestResult<long>();
            try
            {
                _orderStorage.TransactionStart();
                result.RequestData = await _orderStorage.AddOrUpdateOrder(dataModel);
                _orderStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _orderStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }
    }
}
    
