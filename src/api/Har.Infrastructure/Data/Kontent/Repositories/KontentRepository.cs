using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kentico.Kontent.Delivery.Abstractions;

namespace Har.Infrastructure.Data.Kontent.Repositories
{
    public class KontentRepository<TKontentType, TReturnType>
    {
        private readonly IDeliveryClient _deliveryClient;
        private readonly Func<TKontentType, TReturnType> _mapperFunction;

        protected KontentRepository(IDeliveryClient deliveryClient, Func<TKontentType, TReturnType> mapperFunction)
        {
            _deliveryClient = deliveryClient;
            _mapperFunction = mapperFunction;
        }

        protected async Task<TReturnType> GetItemAsync(string codeName)
        {
            var item = await _deliveryClient.GetItemAsync<TKontentType>(codeName);
            return _mapperFunction.Invoke(item.Item);
        }

        protected async Task<IEnumerable<TReturnType>> GetItemsAsync()
        {
            var items = await _deliveryClient.GetItemsAsync<TKontentType>();
            return items.Items.Select(_mapperFunction);
        }
    }
}