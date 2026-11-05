using System.Text;
using System.Threading;
using Portfolio.CleanArchitecture.Application.DI;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.CleanArchitecture.Presentation
{
    /// <summary>
    /// Minimal view that binds the composition root to a Text (or TMP) label.
    /// Attach to a Canvas with a UI Text child referenced below.
    /// </summary>
    public sealed class CourseListView : MonoBehaviour
    {
        [SerializeField] private CompositionRoot compositionRoot;
        [SerializeField] private Text statusText;

        private CancellationTokenSource _cts;

        private async void Start()
        {
            if (compositionRoot == null)
            {
                compositionRoot = FindFirstObjectByType<CompositionRoot>();
            }

            if (compositionRoot == null || statusText == null)
            {
                Debug.LogError("CourseListView is missing references.");
                return;
            }

            _cts = new CancellationTokenSource();
            statusText.text = "Loading courses...";

            try
            {
                var courses = await compositionRoot.Courses.ListCoursesAsync(_cts.Token);
                var builder = new StringBuilder();
                builder.AppendLine("Courses");
                builder.AppendLine("-------");

                foreach (var course in courses)
                {
                    builder.AppendLine($"• {course.Title}");
                    builder.AppendLine($"  {course.Description}");
                    builder.AppendLine($"  Teacher: {course.TeacherName} | Chapters: {course.ChapterCount}");
                    builder.AppendLine();
                }

                statusText.text = builder.ToString();
            }
            catch (System.Exception ex)
            {
                statusText.text = $"Failed to load courses:\n{ex.Message}";
                Debug.LogException(ex);
            }
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}
