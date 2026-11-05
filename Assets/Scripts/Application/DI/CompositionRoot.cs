using Portfolio.CleanArchitecture.Application.Controllers;
using Portfolio.CleanArchitecture.Domain.Ports;
using Portfolio.CleanArchitecture.Infrastructure.Adapters;
using Portfolio.CleanArchitecture.Infrastructure.Services;
using UnityEngine;

namespace Portfolio.CleanArchitecture.Application.DI
{
    /// <summary>
    /// Lightweight composition root. Prefer a DI framework (Zenject/VContainer) in larger projects;
    /// this keeps the demo dependency-free while showing the same wiring intent.
    /// </summary>
    public sealed class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private bool useHttpAdapter;
        [SerializeField] private string apiBaseUrl = "https://example.com/api";

        public CourseController Courses { get; private set; }

        private void Awake()
        {
            ICourseAdapter adapter = useHttpAdapter
                ? new HttpCourseAdapter(new UnityWebRequestHttpClient(), apiBaseUrl)
                : new FakeCourseAdapter();

            Courses = new CourseController(adapter);
        }
    }
}
