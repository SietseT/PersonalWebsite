using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Har.Infrastructure.Data.Kontent.Types;
using Kentico.Kontent.Delivery.Abstractions;
using Moq;

namespace Har.Infrastructure.Tests.Kontent.Repositories
{
    public class FakeProjectsDeliveryClient : IDeliveryClient
    {
        private readonly IEnumerable<Project> _projects;

        public FakeProjectsDeliveryClient(IEnumerable<Project> projects)
        {
            _projects = projects;
        }
        
        public Task<IDeliveryItemResponse<T>> GetItemAsync<T>(string codename, IEnumerable<IQueryParameter> parameters = null)
        {
            var deliveryItemResponseMock = new Mock<IDeliveryItemResponse<Project>>();
            
            var project = _projects
                .First(p => p.System.Codename.Equals(codename, StringComparison.OrdinalIgnoreCase));

            deliveryItemResponseMock
                .SetupGet(d => d.Item)
                .Returns(project);

            return Task.FromResult(deliveryItemResponseMock.Object as IDeliveryItemResponse<T>);
        }

        public Task<IDeliveryItemListingResponse<T>> GetItemsAsync<T>(IEnumerable<IQueryParameter> parameters = null)
        {
            var deliveryItemListingResponseMock = new Mock<IDeliveryItemListingResponse<Project>>();

            var projects = _projects;

            deliveryItemListingResponseMock
                .SetupGet(d => d.Items)
                .Returns(projects.ToList);

            return Task.FromResult(deliveryItemListingResponseMock.Object as IDeliveryItemListingResponse<T>);
        }

        public IDeliveryItemsFeed<T> GetItemsFeed<T>(IEnumerable<IQueryParameter> parameters = null)
        {
            throw new NotImplementedException();
        }

        public Task<IDeliveryTypeResponse> GetTypeAsync(string codename)
        {
            throw new NotImplementedException();
        }

        public Task<IDeliveryTypeListingResponse> GetTypesAsync(IEnumerable<IQueryParameter> parameters = null)
        {
            throw new NotImplementedException();
        }

        public Task<IDeliveryElementResponse> GetContentElementAsync(string contentTypeCodename, string contentElementCodename)
        {
            throw new NotImplementedException();
        }

        public Task<IDeliveryTaxonomyResponse> GetTaxonomyAsync(string codename)
        {
            throw new NotImplementedException();
        }

        public Task<IDeliveryTaxonomyListingResponse> GetTaxonomiesAsync(IEnumerable<IQueryParameter> parameters = null)
        {
            throw new NotImplementedException();
        }
    }
}