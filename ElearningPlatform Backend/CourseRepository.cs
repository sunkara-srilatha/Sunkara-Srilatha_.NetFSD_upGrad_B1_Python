using ElearningPlatform.Data;
using ElearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace ElearningPlatform.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ElearningDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Course>> GetCoursesByCreatorAsync(int creatorId)
        {
            return await _dbSet.AsNoTracking()
                .Include(c => c.Creator)
                .Where(c => c.CreatedBy == creatorId)
                .ToListAsync();
        }

        public async Task<Course> GetCourseWithDetailsAsync(int courseId)
        {
            return await _dbSet.AsNoTracking()
                .Include(c => c.Creator)
                .Include(c => c.Lessons)
                .Include(c => c.Quizzes)
                .FirstOrDefaultAsync(c => c.CourseId == courseId);
        }
    }
}
