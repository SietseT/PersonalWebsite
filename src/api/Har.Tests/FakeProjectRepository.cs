using System.Collections.Generic;
using System.Threading.Tasks;
using Har.Application.Services;
using Har.Domain.Models;

namespace Har.Tests
{
    public class FakeProjectRepository : IProjectRepository
    {
        public Task<Project> GetProjectByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Project>> GetProjectsAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}