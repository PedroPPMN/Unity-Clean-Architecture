using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Portfolio.CleanArchitecture.Domain.Models;

namespace Portfolio.CleanArchitecture.Domain.Ports
{
    /// <summary>
    /// Port that the application layer depends on. Infrastructure provides the adapter.
    /// </summary>
    public interface ICourseAdapter
    {
        Task<IReadOnlyList<CourseModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CourseModel> GetByIdAsync(string courseId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ChapterModel>> GetChaptersAsync(string courseId, CancellationToken cancellationToken = default);
    }
}
