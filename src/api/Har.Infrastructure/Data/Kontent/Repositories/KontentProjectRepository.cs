using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Har.Application.Services;
using Har.Infrastructure.Data.Kontent.Types;
using Kentico.Kontent.Delivery.Abstractions;
using Microsoft.Extensions.Logging;

namespace Har.Infrastructure.Data.Kontent.Repositories
{
    public class KontentProjectRepository : KontentRepository<Project, Har.Domain.Models.Project>, IProjectRepository
    {
        private readonly ILogger<KontentProjectRepository> _logger;

        public KontentProjectRepository(IDeliveryClient deliveryClient, ILogger<KontentProjectRepository> logger) :
            base(deliveryClient, Map)
        {
            _logger = logger;
        }

        public Task<Har.Domain.Models.Project> GetProjectByIdAsync(string id)
        {
            try
            {
                return GetItemAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API error executing {Method} for item with codename {CodeName}",
                    nameof(GetProjectByIdAsync), id);
                return default;
            }
        }

        public Task<IEnumerable<Har.Domain.Models.Project>> GetProjectsAsync()
        {
            try
            {
                return GetItemsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API error executing {Method}", nameof(GetProjectsAsync));
                return default;
            }
        }

        private static Har.Domain.Models.Project Map(Project project)
        {
            return new()
            {
                Name = project.System.Name,
                Url = project.SiteUrl,
                OnlineSince = project.OnlineSince ?? DateTime.MinValue,
                ShortDescription = project.ShortDescription,
                Technologies = project.Technologies
            };
        }
    }
}