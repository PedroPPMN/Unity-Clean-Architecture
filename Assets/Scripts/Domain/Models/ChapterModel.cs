using System;

namespace Portfolio.CleanArchitecture.Domain.Models
{
    [Serializable]
    public sealed class ChapterModel
    {
        public string Id;
        public string CourseId;
        public string Title;
        public int Order;
    }
}
