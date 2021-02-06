using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
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

        public async Task<Har.Domain.Models.Project> GetProjectByIdAsync(string id)
        {
            try
            {
                return await GetItemAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API error executing {Method} for item with codename {CodeName}",
                    nameof(GetProjectByIdAsync), id);
                return default;
            }
        }

        public async Task<IEnumerable<Har.Domain.Models.Project>> GetProjectsAsync()
        {
            try
            {
                return await GetItemsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API error executing {Method}", nameof(GetProjectsAsync));
                return default;
            }
        }

        private static async Task<Har.Domain.Models.Project> Map(Project project)
        {
            return new()
            {
                Id = project.System.Codename,
                Name = project.System.Name,
                Author = project.Author,
                Content = project.Content,
                Url = project.SiteUrl,
                OnlineSince = project.OnlineSince ?? DateTime.MinValue,
                ShortDescription = project.ShortDescription,
                Technologies = await GetTechnologiesFromHtml(project.Technologies)
            };
        }

        private static async Task<IEnumerable<string>> GetTechnologiesFromHtml(string html)
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(html));

            var listItems = document.QuerySelectorAll("li");

            return listItems.Select(l => l.TextContent);
        }
    }
}