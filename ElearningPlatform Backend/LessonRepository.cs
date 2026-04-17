using ElearningPlatform.Data;
using ElearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace ElearningPlatform.Repositories
{
    public class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        public LessonRepository(ElearningDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Lesson>> GetLessonsByCourseAsync(int courseId)
        {
            return await _dbSet.AsNoTracking()
                .Where(l => l.CourseId == courseId)
                .OrderBy(l => l.OrderIndex)
                .ToListAsync();
        }
    }
}
