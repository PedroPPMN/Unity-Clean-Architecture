using System;

namespace Portfolio.CleanArchitecture.Domain.Models
{
    [Serializable]
    public sealed class CourseModel
    {
        public string Id;
        public string Title;
        public string Description;
        public string TeacherName;
        public int ChapterCount;
    }
}
