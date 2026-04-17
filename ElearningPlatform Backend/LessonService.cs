using AutoMapper;
using ElearningPlatform.DTOs;
using ElearningPlatform.Models;
using ElearningPlatform.Repositories;

namespace ElearningPlatform.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public LessonService(ILessonRepository lessonRepository, ICourseRepository courseRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<LessonDto> CreateLessonAsync(CreateLessonDto createLessonDto)
        {
            // Verify that the course exists
            if (!await _courseRepository.ExistsAsync(createLessonDto.CourseId))
            {
                throw new KeyNotFoundException($"Course with ID {createLessonDto.CourseId} not found");
            }

            var lesson = _mapper.Map<Lesson>(createLessonDto);
            var createdLesson = await _lessonRepository.AddAsync(lesson);
            
            return _mapper.Map<LessonDto>(createdLesson);
        }

        public async Task<LessonDto> GetLessonByIdAsync(int lessonId)
        {
            var lesson = await _lessonRepository.GetByIdAsync(lessonId);
            if (lesson == null)
            {
                throw new KeyNotFoundException($"Lesson with ID {lessonId} not found");
            }

            return _mapper.Map<LessonDto>(lesson);
        }

        public async Task<IEnumerable<LessonDto>> GetLessonsByCourseAsync(int courseId)
        {
            // Verify that the course exists
            if (!await _courseRepository.ExistsAsync(courseId))
            {
                throw new KeyNotFoundException($"Course with ID {courseId} not found");
            }

            var lessons = await _lessonRepository.GetLessonsByCourseAsync(courseId);
            return _mapper.Map<IEnumerable<LessonDto>>(lessons);
        }

        public async Task<LessonDto> UpdateLessonAsync(int lessonId, UpdateLessonDto updateLessonDto)
        {
            var existingLesson = await _lessonRepository.GetByIdAsync(lessonId);
            if (existingLesson == null)
            {
                throw new KeyNotFoundException($"Lesson with ID {lessonId} not found");
            }

            _mapper.Map(updateLessonDto, existingLesson);
            var updatedLesson = await _lessonRepository.UpdateAsync(existingLesson);
            
            return _mapper.Map<LessonDto>(updatedLesson);
        }

        public async Task<bool> DeleteLessonAsync(int lessonId)
        {
            var lesson = await _lessonRepository.DeleteAsync(lessonId);
            return lesson != null;
        }
    }
}
