using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Portfolio.CleanArchitecture.Domain.Models;
using Portfolio.CleanArchitecture.Domain.Ports;

namespace Portfolio.CleanArchitecture.Application.Controllers
{
    /// <summary>
    /// Application use-case orchestrator. Views talk to this, never to adapters directly.
    /// </summary>
    public sealed class CourseController
    {
        private readonly ICourseAdapter _courseAdapter;

        public CourseController(ICourseAdapter courseAdapter)
        {
            _courseAdapter = courseAdapter ?? throw new ArgumentNullException(nameof(courseAdapter));
        }

        public Task<IReadOnlyList<CourseModel>> ListCoursesAsync(CancellationToken cancellationToken = default)
            => _courseAdapter.GetAllAsync(cancellationToken);

        public Task<CourseModel> GetCourseAsync(string courseId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(courseId))
            {
                throw new ArgumentException("Course id is required.", nameof(courseId));
            }

            return _courseAdapter.GetByIdAsync(courseId, cancellationToken);
        }

        public Task<IReadOnlyList<ChapterModel>> ListChaptersAsync(string courseId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(courseId))
            {
                throw new ArgumentException("Course id is required.", nameof(courseId));
            }

            return _courseAdapter.GetChaptersAsync(courseId, cancellationToken);
        }
    }
}
