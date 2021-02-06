using System.Collections.Generic;
using System.Threading.Tasks;
using Har.Domain.Models;

namespace Har.Application.Services
{
    public interface IProjectRepository
    {
        Task<Project> GetProjectByIdAsync(string id);
        Task<IEnumerable<Project>> GetProjectsAsync();
    }
}