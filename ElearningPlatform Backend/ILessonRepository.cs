using ElearningPlatform.Models;

namespace ElearningPlatform.Repositories
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Task<IEnumerable<Lesson>> GetLessonsByCourseAsync(int courseId);
    }
}
