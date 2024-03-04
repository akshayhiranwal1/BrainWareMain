using System;
using Api.Models;

namespace Api.Interfaces
{
	public interface IOrder
	{
        public Task<List<OrderSummary>> GetOrders();
    }
}

