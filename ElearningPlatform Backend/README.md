# E-Learning Platform Backend

A comprehensive ASP.NET Core 8 Web API for managing multi-page e-learning courses, quizzes, and user progress tracking.

## Features

- **User Management**: Registration, authentication, and profile management
- **Course Management**: Create, read, update, and delete courses
- **Lesson Management**: Organize course content with ordered lessons
- **Quiz System**: Create quizzes with multiple-choice questions
- **Progress Tracking**: Track user quiz results and performance
- **RESTful APIs**: Clean, well-documented API endpoints
- **Security**: Password hashing and input validation
- **Database**: SQL Server with Entity Framework Core

## Database Schema

### Tables
- **Users**: User accounts with authentication
- **Courses**: Course information and metadata
- **Lessons**: Course content organized by order
- **Quizzes**: Assessments for courses
- **Questions**: Multiple-choice quiz questions
- **Results**: User quiz attempts and scores

### Relationships
- One User can create Many Courses
- One Course has Many Lessons
- One Course has Many Quizzes
- One Quiz has Many Questions
- One User has Many Results

## API Endpoints

### Users
- `POST /api/users/register` - Register new user
- `POST /api/users/login` - User login
- `GET /api/users/{id}` - Get user by ID
- `PUT /api/users/{id}` - Update user
- `DELETE /api/users/{id}` - Delete user
- `GET /api/users/check-email/{email}` - Check if email exists

### Courses
- `GET /api/courses` - Get all courses
- `GET /api/courses/{id}` - Get course by ID
- `GET /api/courses/{id}/details` - Get course with lessons and quizzes
- `POST /api/courses` - Create new course
- `PUT /api/courses/{id}` - Update course
- `DELETE /api/courses/{id}` - Delete course
- `GET /api/courses/creator/{creatorId}` - Get courses by creator

### Lessons
- `GET /api/lessons/{id}` - Get lesson by ID
- `GET /api/lessons/course/{courseId}` - Get lessons by course
- `POST /api/lessons` - Create new lesson
- `PUT /api/lessons/{id}` - Update lesson
- `DELETE /api/lessons/{id}` - Delete lesson

### Quizzes
- `GET /api/quizzes/{courseId}` - Get quizzes by course
- `GET /api/quizzes/quiz/{quizId}` - Get quiz by ID
- `GET /api/quizzes/{quizId}/questions` - Get quiz with questions
- `POST /api/quizzes` - Create new quiz
- `PUT /api/quizzes/{quizId}` - Update quiz
- `DELETE /api/quizzes/{quizId}` - Delete quiz

### Questions
- `GET /api/questions/{id}` - Get question by ID
- `GET /api/questions/quiz/{quizId}` - Get questions by quiz
- `POST /api/questions` - Create new question
- `PUT /api/questions/{id}` - Update question
- `DELETE /api/questions/{id}` - Delete question

### Results
- `POST /api/results/quiz/{quizId}/submit?userId={userId}` - Submit quiz attempt
- `GET /api/results/{resultId}` - Get result by ID
- `GET /api/results/user/{userId}` - Get results by user
- `GET /api/results/quiz/{quizId}/results` - Get results by quiz
- `DELETE /api/results/{resultId}` - Delete result

## Setup Instructions

### Prerequisites
- .NET 8.0 SDK
- SQL Server (Express or LocalDB)
- Visual Studio 2022 or VS Code

### Database Setup

1. **SQL Server Express** (Recommended):
   - Install SQL Server Express with Advanced Services
   - Ensure SQL Server Browser service is running
   - Update connection string in `appsettings.json`:
   ```json
   "DefaultConnection": "Server=.\\SQLEXPRESS;Database=ElearningDB;Trusted_Connection=true;TrustServerCertificate=true;"
   ```

2. **SQL Server LocalDB** (Alternative):
   - Install SQL Server Express with LocalDB
   - Update connection string in `appsettings.json`:
   ```json
   "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ElearningDB;Trusted_Connection=true;TrustServerCertificate=true;"
   ```

3. **SQL Server Standard/Developer**:
   - Update connection string with your server details:
   ```json
   "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=ElearningDB;Trusted_Connection=true;TrustServerCertificate=true;"
   ```

### Application Setup

1. Clone or download the project
2. Navigate to the project directory
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Build the project:
   ```bash
   dotnet build
   ```
5. Create and apply database migrations:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
6. Run the application:
   ```bash
   dotnet run
   ```

### Testing

The application includes Swagger UI for API testing:
- Navigate to `https://localhost:xxxx/swagger` when running
- Use the interactive API documentation to test endpoints

## Sample API Usage

### Register a User
```bash
POST /api/users/register
{
  "fullName": "John Doe",
  "email": "john@example.com",
  "password": "Password123"
}
```

### Create a Course
```bash
POST /api/courses
{
  "title": "Introduction to C#",
  "description": "Learn C# programming fundamentals",
  "createdBy": 1
}
```

### Create a Quiz
```bash
POST /api/quizzes
{
  "courseId": 1,
  "title": "C# Basics Quiz"
}
```

### Submit Quiz
```bash
POST /api/results/quiz/1/submit?userId=1
{
  "quizId": 1,
  "answers": [
    {
      "questionId": 1,
      "selectedAnswer": "A"
    },
    {
      "questionId": 2,
      "selectedAnswer": "B"
    }
  ]
}
```

## Architecture

The application follows a clean, layered architecture:

- **Controllers**: Handle HTTP requests and responses
- **Services**: Business logic and data processing
- **Repositories**: Data access layer using Entity Framework Core
- **DTOs**: Data transfer objects for API communication
- **Models**: Entity Framework database entities
- **Data**: Database context and configuration

## Security Features

- Password hashing using SHA256 with salt
- Input validation and error handling
- Proper HTTP status codes
- DTOs prevent direct entity exposure
- SQL injection prevention through EF Core

## Performance Features

- AsNoTracking() for read operations
- Async/await pattern throughout
- Efficient database queries with proper includes
- Repository pattern for data access optimization

## Technologies Used

- ASP.NET Core 8.0
- Entity Framework Core 8.0
- SQL Server
- AutoMapper 12.0
- Swagger/OpenAPI
- Repository Pattern
- Dependency Injection

## Database Queries

The application implements various SQL query patterns:
- Basic CRUD operations
- JOIN operations for related data
- Aggregation for statistics
- Filtering and sorting
- Subqueries for complex scenarios

## Testing

Unit tests can be added to verify:
- CRUD operations
- Quiz scoring logic
- User authentication
- Data validation
- API responses
