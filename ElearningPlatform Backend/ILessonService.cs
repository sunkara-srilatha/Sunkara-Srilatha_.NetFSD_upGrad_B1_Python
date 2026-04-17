using ElearningPlatform.DTOs;

namespace ElearningPlatform.Services
{
    public interface ILessonService
    {
        Task<LessonDto> CreateLessonAsync(CreateLessonDto createLessonDto);
        Task<LessonDto> GetLessonByIdAsync(int lessonId);
        Task<IEnumerable<LessonDto>> GetLessonsByCourseAsync(int courseId);
        Task<LessonDto> UpdateLessonAsync(int lessonId, UpdateLessonDto updateLessonDto);
        Task<bool> DeleteLessonAsync(int lessonId);
    }
}
