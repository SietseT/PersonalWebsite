using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Har.Application.Services;
using Har.Domain.Models;

namespace Har.Tests
{
    public class FakeProjectRepository : IProjectRepository
    {
        public Task<Project> GetProjectByIdAsync(string id)
        {
            return Task.FromResult(GetProjects()
                .FirstOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase)));
        }

        public Task<IEnumerable<Project>> GetProjectsAsync()
        {
            return Task.FromResult(GetProjects());
        }

        private static IEnumerable<Project> GetProjects()
        {
            return new Project[]
            {
                new()
                {
                    Id = "elunduscore",
                    Name = "Elundus Core",
                    Author = "Sietse Trommelen",
                    Url = "https://elunduscore.com",
                    OnlineSince = new DateTime(2019, 7, 1),
                    ShortDescription =
                        "Initially created as a hobby project to try out YAML for Azure Pipelines and mess around with a text-to-speech service, has grown to a website with thousands of daily users.",
                    Content = "<p>Text before</p>\n<p><br></p>\n<div class=\"image-slider\">\n<img src=\"https://assets-eu-01.kc-usercontent.com:443/7f506e42-9290-01aa-7e18-b07c3b2d973c/0ee58a9c-48c5-433f-aa89-671d665a6ce3/msedge_2DBLUL4Hgp.jpg\" alt=\"Elundus Core desktop view\">\n<img src=\"https://assets-eu-01.kc-usercontent.com:443/7f506e42-9290-01aa-7e18-b07c3b2d973c/e4a8b4e5-86c4-4b56-9557-bd4aa1fc53f1/msedge_4Ig7LdiDyX.jpg\" alt=\"Elundus Core mobile view\">\n</div>\n\n<p><br></p>\n<h1>Elundus Core</h1>\n<p>Elundus Core is a website where you can convert text-to-speech using <a href=\"https://aws.amazon.com/polly/\" data-new-window=\"true\" target=\"_blank\" rel=\"noopener noreferrer\">Amazon Polly</a>. I've started building this tool when I used to watch <a href=\"https://www.twitch.tv/\" data-new-window=\"true\" target=\"_blank\" rel=\"noopener noreferrer\">Twitch</a> streams regularly.&nbsp;</p>\n<p><br></p>",
                    Technologies = new[]
                    {
                        ".NET Core",
                        "Gatsby",
                        "React",
                        "Azure"
                    }
                },
                new()
                {
                    Id = "sietsetrommelendev",
                    Name = "sietsetrommelen.dev (Personal website)",
                    Author = "Sietse Trommelen",
                    Url = "https://sietsetrommelen.dev",
                    OnlineSince = new DateTime(2021, 4, 1),
                    ShortDescription =
                        "Personal website heavily over-engineered to expirement with techniques I haven't worked with yet.",
                    Content = "This is some content.",
                    Technologies = new[]
                    {
                        ".NET Core",
                        "NextJS",
                        "GraphQL",
                        "Docker",
                        "GraphQL"
                    }
                },
            };
        }
    }
}