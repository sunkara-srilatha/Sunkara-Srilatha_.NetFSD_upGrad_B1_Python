# Multi-Page E-Learning Platform

A comprehensive client-side e-learning platform built with HTML, CSS, and JavaScript that provides structured online courses, quizzes, and learner progress tracking without requiring a backend server.

## Features

### 📚 Core Features
- **Multi-page Navigation**: Dashboard, Courses, Quiz, and Profile pages
- **Course Management**: Browse and track course progress
- **Interactive Quizzes**: Dynamic quiz generation with real-time feedback
- **Progress Tracking**: Visual progress bars and achievement tracking
- **Profile Management**: Personal learning dashboard with statistics

### 🎨 UI/UX Features
- **Responsive Design**: Mobile-friendly layout with media queries
- **Modern Styling**: Gradient backgrounds, smooth animations, and transitions
- **Semantic HTML**: Proper use of HTML5 semantic elements
- **CSS Grid & Flexbox**: Advanced layout techniques
- **Interactive Elements**: Hover effects, loading animations, and micro-interactions

### 🔧 Technical Features
- **Web Storage**: LocalStorage for data persistence
- **Asynchronous JavaScript**: Promise-based quiz loading simulation
- **DOM Manipulation**: Dynamic content generation
- **State Management**: Multi-page state synchronization
- **Testing**: Jest test suite for core functionality

## Project Structure

```
Multi Page E-Learning Platform/
├── Dashboard.html          # Main dashboard with overview
├── Courses.html            # Course catalog and details
├── Quiz.html               # Interactive quiz system
├── Profile.html            # User profile and statistics
├── styles.css              # Complete styling with responsive design
├── script.js               # Core JavaScript functionality
├── package.json            # Project dependencies and scripts
├── README.md               # Project documentation
└── tests/
    ├── grade.test.js       # Jest test cases
    └── setup.js            # Test configuration
```

## Pages Overview

### 🏠 Dashboard.html
- **Learning Progress**: Overall course completion percentage
- **Achievements**: Courses completed, quizzes passed, total score
- **Recent Activity**: Last quiz, current streak, study time
- **Recommended Courses**: Personalized course suggestions

### 📖 Courses.html
- **Course Table**: Comprehensive list with filtering options
- **Course Cards**: Detailed view with lessons and duration
- **Progress Tracking**: Visual indicators for course status
- **Interactive Actions**: Start, continue, and mark complete

### 📝 Quiz.html
- **Dynamic Loading**: Async quiz question loading simulation
- **Interactive Questions**: Radio button selection with navigation
- **Progress Tracking**: Real-time quiz progress bar
- **Results Analysis**: Detailed feedback and answer review

### 👤 Profile.html
- **Personal Information**: Editable profile details
- **Learning Statistics**: Comprehensive performance metrics
- **Completed Courses**: Visual list of achievements
- **Recent Activity**: Timeline of learning actions

## Technical Implementation

### HTML Structure
- Semantic HTML5 elements (`<header>`, `<nav>`, `<main>`, `<section>`, `<footer>`)
- Breadcrumb navigation on all pages
- Hierarchical linking between pages
- HTML tables for course data display
- Ordered lists for lesson organization

### CSS Features
- **CSS Grid**: Dashboard layout system
- **Flexbox**: Course card arrangement
- **Advanced Selectors**: Attribute selectors, pseudo-classes, combinators
- **Media Queries**: Responsive design for mobile devices
- **Custom Properties**: Consistent theming
- **Animations**: Smooth transitions and loading states

### JavaScript Implementation

#### Arrays & Objects
- Course data stored as JavaScript objects
- Quiz questions in array of objects
- Dynamic data manipulation and filtering

#### Control Flow
- **If-Else**: Grade calculation logic
- **Switch Statement**: Performance feedback messages
- **Loops**: Dynamic content generation

#### DOM Manipulation
- Dynamic quiz question generation
- Real-time option rendering
- Results section display after submission
- Interactive course cards and tables

#### Asynchronous JavaScript
- **Promise**: Quiz loading simulation
- **Async/Await**: Modern async handling
- **setTimeout**: Loading state simulation

#### Web Storage
- LocalStorage for quiz results
- Course completion tracking
- Multi-page state management
- User preference persistence

#### Events
- **onclick**: Quiz submission and navigation
- **onchange**: Answer selection handling
- **DOMContentLoaded**: Page initialization

#### Progress Elements
- HTML5 `<progress>` element usage
- Course completion visualization
- Quiz progress tracking

## Testing

### Jest Test Cases
The project includes comprehensive test coverage for:

1. **Grade Calculation Logic**
   - A+ through F grade boundaries
   - Edge case handling
   - Input validation

2. **Score Percentage Calculation**
   - Various score scenarios
   - Decimal precision handling
   - Edge cases (zero division)

3. **Pass/Fail Determination**
   - Threshold-based evaluation
   - Custom threshold support
   - Realistic quiz scenarios

### Running Tests
```bash
# Install dependencies
npm install

# Run tests
npm test

# Run tests in watch mode
npm run test:watch
```

## Getting Started

### Prerequisites
- Modern web browser (Chrome, Firefox, Safari, Edge)
- Node.js (for testing only)
- Local web server (optional)

### Installation
1. Clone or download the project
2. Navigate to the project directory
3. Install dependencies for testing:
   ```bash
   npm install
   ```

### Running the Application

#### Option 1: Direct File Access
Simply open `Dashboard.html` in your web browser.

#### Option 2: Local Server
```bash
# Using Python
python -m http.server 8000

# Using Node.js serve package
npx serve .

# Using the included npm script
npm start
```

Then open `http://localhost:8000` in your browser.

## Usage Guide

### First Time Setup
1. Open the Dashboard page
2. Your profile is automatically initialized
3. Browse available courses
4. Start your learning journey!

### Taking a Quiz
1. Navigate to the Quiz page
2. Wait for questions to load (2-second simulation)
3. Answer each question using radio buttons
4. Navigate between questions using Previous/Next
5. Submit to see detailed results
6. Review your answers and feedback

### Managing Courses
1. Visit the Courses page
2. Browse the course catalog
3. Click "Start Course" to begin
4. Track your progress on the Dashboard
5. Mark courses as complete

### Viewing Profile
1. Go to the Profile page
2. View your learning statistics
3. Check completed courses
4. Monitor quiz performance
5. Edit your profile information

## Browser Compatibility

- ✅ Chrome 60+
- ✅ Firefox 55+
- ✅ Safari 12+
- ✅ Edge 79+

## Features Demonstrated

### Academic Requirements Met
- ✅ 4 HTML pages with semantic structure
- ✅ CSS Grid and Flexbox layouts
- ✅ Responsive design with media queries
- ✅ Advanced CSS selectors
- ✅ JavaScript arrays and objects
- ✅ Control flow (if-else, switch)
- ✅ DOM manipulation
- ✅ Asynchronous JavaScript
- ✅ Web Storage implementation
- ✅ Event handling
- ✅ Progress elements
- ✅ Jest test suite (3+ test cases)

### Additional Features
- ✅ Modern UI with gradients and animations
- ✅ Loading states and micro-interactions
- ✅ Comprehensive error handling
- ✅ Accessibility considerations
- ✅ Performance optimizations
- ✅ Cross-browser compatibility

## Contributing

This project serves as a comprehensive demonstration of frontend web development capabilities. Feel free to extend it with additional features or use it as a learning resource.

## License

MIT License - Feel free to use this project for educational purposes.
