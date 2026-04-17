using ElearningPlatform.Models;

namespace ElearningPlatform.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetCoursesByCreatorAsync(int creatorId);
        Task<Course> GetCourseWithDetailsAsync(int courseId);
    }
}
