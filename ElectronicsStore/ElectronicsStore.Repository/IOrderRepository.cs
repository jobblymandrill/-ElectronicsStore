﻿using ElecronicsStore.DB.Models;
using System.Threading.Tasks;

namespace ElectronicsStore.Repository
{
    public interface IOrderRepository
    {
        ValueTask<RequestResult<Order>> AddOrUpdateOrder(Order dataModel);
    }
}