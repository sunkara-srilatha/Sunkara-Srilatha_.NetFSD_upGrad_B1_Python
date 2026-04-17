using AutoMapper;
using ElearningPlatform.DTOs;
using ElearningPlatform.Models;
using ElearningPlatform.Repositories;

namespace ElearningPlatform.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IUserRepository userRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto)
        {
            // Verify that the creator exists
            if (!await _userRepository.ExistsAsync(createCourseDto.CreatedBy))
            {
                throw new KeyNotFoundException($"User with ID {createCourseDto.CreatedBy} not found");
            }

            var course = _mapper.Map<Course>(createCourseDto);
            var createdCourse = await _courseRepository.AddAsync(course);
            
            return _mapper.Map<CourseDto>(createdCourse);
        }

        public async Task<CourseDto> GetCourseByIdAsync(int courseId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
            {
                throw new KeyNotFoundException($"Course with ID {courseId} not found");
            }

            return _mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDetailDto> GetCourseWithDetailsAsync(int courseId)
        {
            var course = await _courseRepository.GetCourseWithDetailsAsync(courseId);
            if (course == null)
            {
                throw new KeyNotFoundException($"Course with ID {courseId} not found");
            }

            return _mapper.Map<CourseDetailDto>(course);
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDto> UpdateCourseAsync(int courseId, UpdateCourseDto updateCourseDto)
        {
            var existingCourse = await _courseRepository.GetByIdAsync(courseId);
            if (existingCourse == null)
            {
                throw new KeyNotFoundException($"Course with ID {courseId} not found");
            }

            _mapper.Map(updateCourseDto, existingCourse);
            var updatedCourse = await _courseRepository.UpdateAsync(existingCourse);
            
            return _mapper.Map<CourseDto>(updatedCourse);
        }

        public async Task<bool> DeleteCourseAsync(int courseId)
        {
            var course = await _courseRepository.DeleteAsync(courseId);
            return course != null;
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByCreatorAsync(int creatorId)
        {
            var courses = await _courseRepository.GetCoursesByCreatorAsync(creatorId);
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }
    }
}
