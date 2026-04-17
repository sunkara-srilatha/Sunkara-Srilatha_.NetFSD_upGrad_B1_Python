using ElearningPlatform.DTOs;

namespace ElearningPlatform.Services
{
    public interface ICourseService
    {
        Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto);
        Task<CourseDto> GetCourseByIdAsync(int courseId);
        Task<CourseDetailDto> GetCourseWithDetailsAsync(int courseId);
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> UpdateCourseAsync(int courseId, UpdateCourseDto updateCourseDto);
        Task<bool> DeleteCourseAsync(int courseId);
        Task<IEnumerable<CourseDto>> GetCoursesByCreatorAsync(int creatorId);
    }
}
