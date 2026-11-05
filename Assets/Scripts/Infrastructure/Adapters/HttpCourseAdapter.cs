using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Portfolio.CleanArchitecture.Domain.Models;
using Portfolio.CleanArchitecture.Domain.Ports;
using Portfolio.CleanArchitecture.Infrastructure.Services;
using UnityEngine;

namespace Portfolio.CleanArchitecture.Infrastructure.Adapters
{
    /// <summary>
    /// Production-shaped adapter. Points at a configurable base URL and deserializes JSON.
    /// Wire this instead of FakeCourseAdapter when a backend is available.
    /// </summary>
    public sealed class HttpCourseAdapter : ICourseAdapter
    {
        private readonly ISimpleHttpClient _http;
        private readonly string _baseUrl;

        public HttpCourseAdapter(ISimpleHttpClient http, string baseUrl)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _baseUrl = (baseUrl ?? string.Empty).TrimEnd('/');
        }

        public async Task<IReadOnlyList<CourseModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var json = await _http.GetAsync($"{_baseUrl}/courses", cancellationToken);
            var wrapper = JsonUtility.FromJson<CourseListDto>(WrapArray(json));
            return wrapper.items ?? Array.Empty<CourseModel>();
        }

        public async Task<CourseModel> GetByIdAsync(string courseId, CancellationToken cancellationToken = default)
        {
            var json = await _http.GetAsync($"{_baseUrl}/courses/{courseId}", cancellationToken);
            return JsonUtility.FromJson<CourseModel>(json);
        }

        public async Task<IReadOnlyList<ChapterModel>> GetChaptersAsync(string courseId, CancellationToken cancellationToken = default)
        {
            var json = await _http.GetAsync($"{_baseUrl}/courses/{courseId}/chapters", cancellationToken);
            var wrapper = JsonUtility.FromJson<ChapterListDto>(WrapArray(json));
            return wrapper.items ?? Array.Empty<ChapterModel>();
        }

        // Unity's JsonUtility cannot deserialize top-level arrays.
        private static string WrapArray(string json) => $"{{\"items\":{json}}}";

        [Serializable]
        private sealed class CourseListDto
        {
            public CourseModel[] items;
        }

        [Serializable]
        private sealed class ChapterListDto
        {
            public ChapterModel[] items;
        }
    }
}
