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
        private readonly Func<TKontentType, Task<TReturnType>> _mapperFunction;

        protected KontentRepository(IDeliveryClient deliveryClient, Func<TKontentType, Task<TReturnType>> mapperFunction)
        {
            _deliveryClient = deliveryClient;
            _mapperFunction = mapperFunction;
        }

        protected async Task<TReturnType> GetItemAsync(string codeName)
        {
            var response = await _deliveryClient.GetItemAsync<TKontentType>(codeName);

            if (response.Item == null)
                return default;
            
            return await _mapperFunction.Invoke(response.Item);
        }

        protected async Task<IEnumerable<TReturnType>> GetItemsAsync()
        {
            var items = await _deliveryClient.GetItemsAsync<TKontentType>();
            return await Task.WhenAll(items.Items.Select(async item => await _mapperFunction.Invoke(item)));
        }
    }
}