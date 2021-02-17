using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using Har.Infrastructure.Data.Kontent.Types;

namespace Har.Infrastructure.Data.Kontent.Mappers
{
    public static class KontentMappers
    {
        public static async Task<Har.Domain.Models.Project> MapProject(Project project)
        {
            return new()
            {
                Id = project.System.Codename,
                Name = project.System.Name,
                Author = project.Author,
                Content = project.Content,
                Url = project.SiteUrl,
                OnlineSince = project.OnlineSince,
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