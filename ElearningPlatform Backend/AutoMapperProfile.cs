using AutoMapper;
using ElearningPlatform.Models;

namespace ElearningPlatform.DTOs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User mappings
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Will be set in service

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Email)));

            // Course mappings
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.Creator != null ? src.Creator.FullName : ""))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.LessonCount, opt => opt.MapFrom(src => src.Lessons != null ? src.Lessons.Count : 0))
                .ForMember(dest => dest.QuizCount, opt => opt.MapFrom(src => src.Quizzes != null ? src.Quizzes.Count : 0));

            CreateMap<CreateCourseDto, Course>()
                .ForMember(dest => dest.CourseId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<UpdateCourseDto, Course>()
                .ForMember(dest => dest.CourseId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<Course, CourseDetailDto>()
                .IncludeBase<Course, CourseDto>();

            // Lesson mappings
            CreateMap<Lesson, LessonDto>()
                .ForMember(dest => dest.LessonId, opt => opt.MapFrom(src => src.LessonId))
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.OrderIndex, opt => opt.MapFrom(src => src.OrderIndex));

            CreateMap<CreateLessonDto, Lesson>()
                .ForMember(dest => dest.LessonId, opt => opt.Ignore());

            CreateMap<UpdateLessonDto, Lesson>()
                .ForMember(dest => dest.LessonId, opt => opt.Ignore())
                .ForMember(dest => dest.CourseId, opt => opt.Ignore());

            // Quiz mappings
            CreateMap<Quiz, QuizDto>()
                .ForMember(dest => dest.QuizId, opt => opt.MapFrom(src => src.QuizId))
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.QuestionCount, opt => opt.MapFrom(src => src.Questions != null ? src.Questions.Count : 0));

            CreateMap<CreateQuizDto, Quiz>()
                .ForMember(dest => dest.QuizId, opt => opt.Ignore());

            CreateMap<UpdateQuizDto, Quiz>()
                .ForMember(dest => dest.QuizId, opt => opt.Ignore())
                .ForMember(dest => dest.CourseId, opt => opt.Ignore());

            CreateMap<Quiz, QuizDetailDto>()
                .IncludeBase<Quiz, QuizDto>();

            // Question mappings
            CreateMap<Question, QuestionDto>()
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.QuizId, opt => opt.MapFrom(src => src.QuizId))
                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.QuestionText))
                .ForMember(dest => dest.OptionA, opt => opt.MapFrom(src => src.OptionA))
                .ForMember(dest => dest.OptionB, opt => opt.MapFrom(src => src.OptionB))
                .ForMember(dest => dest.OptionC, opt => opt.MapFrom(src => src.OptionC))
                .ForMember(dest => dest.OptionD, opt => opt.MapFrom(src => src.OptionD));

            CreateMap<CreateQuestionDto, Question>()
                .ForMember(dest => dest.QuestionId, opt => opt.Ignore());

            CreateMap<UpdateQuestionDto, Question>()
                .ForMember(dest => dest.QuestionId, opt => opt.Ignore())
                .ForMember(dest => dest.QuizId, opt => opt.Ignore());

            // Result mappings
            CreateMap<Result, ResultDto>()
                .ForMember(dest => dest.ResultId, opt => opt.MapFrom(src => src.ResultId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : ""))
                .ForMember(dest => dest.QuizId, opt => opt.MapFrom(src => src.QuizId))
                .ForMember(dest => dest.QuizTitle, opt => opt.MapFrom(src => src.Quiz != null ? src.Quiz.Title : ""))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score))
                .ForMember(dest => dest.TotalQuestions, opt => opt.MapFrom(src => src.Quiz != null && src.Quiz.Questions != null ? src.Quiz.Questions.Count : 0))
                .ForMember(dest => dest.Percentage, opt => opt.MapFrom(src => CalculatePercentage(src)))
                .ForMember(dest => dest.AttemptDate, opt => opt.MapFrom(src => src.AttemptDate));

            CreateMap<CreateResultDto, Result>()
                .ForMember(dest => dest.ResultId, opt => opt.Ignore())
                .ForMember(dest => dest.AttemptDate, opt => opt.MapFrom(src => DateTime.Now));
        }

        private static decimal CalculatePercentage(Result result)
        {
            if (result.Quiz == null || result.Quiz.Questions == null || result.Quiz.Questions.Count == 0)
                return 0;
            
            return (decimal)result.Score / result.Quiz.Questions.Count * 100;
        }
    }
}
