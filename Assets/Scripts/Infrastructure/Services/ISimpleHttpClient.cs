using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.CleanArchitecture.Infrastructure.Services
{
    public interface ISimpleHttpClient
    {
        Task<string> GetAsync(string url, CancellationToken cancellationToken = default);
    }
}
