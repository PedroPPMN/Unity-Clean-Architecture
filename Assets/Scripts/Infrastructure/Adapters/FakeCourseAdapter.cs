using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Portfolio.CleanArchitecture.Domain.Models;
using Portfolio.CleanArchitecture.Domain.Ports;
using UnityEngine;

namespace Portfolio.CleanArchitecture.Infrastructure.Adapters
{
    /// <summary>
    /// In-memory adapter used by the demo scene. Swap for HttpCourseAdapter in production.
    /// </summary>
    public sealed class FakeCourseAdapter : ICourseAdapter
    {
        private readonly List<CourseModel> _courses = new()
        {
            new CourseModel
            {
                Id = "course-architecture",
                Title = "Clean Architecture in Unity",
                Description = "Ports, adapters, and dependency inversion for game clients.",
                TeacherName = "Pedro Paulo",
                ChapterCount = 3
            },
            new CourseModel
            {
                Id = "course-networking",
                Title = "Multiplayer Foundations",
                Description = "Lobbies, allocation, and authoritative server patterns.",
                TeacherName = "Pedro Paulo",
                ChapterCount = 4
            },
            new CourseModel
            {
                Id = "course-ux",
                Title = "Spatial UI Patterns",
                Description = "State machines and selection flows for immersive clients.",
                TeacherName = "Pedro Paulo",
                ChapterCount = 2
            }
        };

        private readonly List<ChapterModel> _chapters = new()
        {
            new ChapterModel { Id = "c1", CourseId = "course-architecture", Title = "Why Ports?", Order = 1 },
            new ChapterModel { Id = "c2", CourseId = "course-architecture", Title = "Adapters", Order = 2 },
            new ChapterModel { Id = "c3", CourseId = "course-architecture", Title = "Composition Root", Order = 3 },
            new ChapterModel { Id = "n1", CourseId = "course-networking", Title = "Lobby Flow", Order = 1 },
            new ChapterModel { Id = "n2", CourseId = "course-networking", Title = "Relay", Order = 2 },
            new ChapterModel { Id = "n3", CourseId = "course-networking", Title = "Authority", Order = 3 },
            new ChapterModel { Id = "n4", CourseId = "course-networking", Title = "Reconnect", Order = 4 },
            new ChapterModel { Id = "u1", CourseId = "course-ux", Title = "State Machines", Order = 1 },
            new ChapterModel { Id = "u2", CourseId = "course-ux", Title = "Selection Screens", Order = 2 }
        };

        public async Task<IReadOnlyList<CourseModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            await SimulateLatency(cancellationToken);
            return _courses;
        }

        public async Task<CourseModel> GetByIdAsync(string courseId, CancellationToken cancellationToken = default)
        {
            await SimulateLatency(cancellationToken);
            return _courses.FirstOrDefault(c => c.Id == courseId);
        }

        public async Task<IReadOnlyList<ChapterModel>> GetChaptersAsync(string courseId, CancellationToken cancellationToken = default)
        {
            await SimulateLatency(cancellationToken);
            return _chapters.Where(c => c.CourseId == courseId).OrderBy(c => c.Order).ToList();
        }

        private static async Task SimulateLatency(CancellationToken cancellationToken)
        {
            // Tiny delay so the demo feels async without requiring UniTask.
            await Task.Delay(150, cancellationToken);
        }
    }
}
