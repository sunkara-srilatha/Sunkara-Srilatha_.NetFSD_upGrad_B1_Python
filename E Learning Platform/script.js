// E-Learning Platform JavaScript
// Global data structures for courses and quiz questions

// Course data as JavaScript objects
const courses = [
    {
        id: 1,
        title: "Introduction to Web Development",
        description: "Learn the fundamentals of HTML, CSS, and JavaScript to build modern websites.",
        category: "Web Development",
        difficulty: "beginner",
        duration: "6 weeks",
        instructor: "Dr. Sarah Johnson",
        lessons: [
            { id: 1, title: "HTML Basics", duration: "45 minutes" },
            { id: 2, title: "CSS Fundamentals", duration: "60 minutes" },
            { id: 3, title: "JavaScript Introduction", duration: "90 minutes" },
            { id: 4, title: "Responsive Design", duration: "75 minutes" },
            { id: 5, title: "Project: Portfolio Website", duration: "120 minutes" }
        ]
    },
    {
        id: 2,
        title: "Advanced JavaScript",
        description: "Master advanced JavaScript concepts including ES6+, async programming, and frameworks.",
        category: "Programming",
        difficulty: "advanced",
        duration: "8 weeks",
        instructor: "Prof. Michael Chen",
        lessons: [
            { id: 1, title: "ES6+ Features", duration: "90 minutes" },
            { id: 2, title: "Async Programming", duration: "85 minutes" },
            { id: 3, title: "DOM Manipulation", duration: "70 minutes" },
            { id: 4, title: "Event Handling", duration: "65 minutes" },
            { id: 5, title: "Modern Frameworks", duration: "100 minutes" }
        ]
    },
    {
        id: 3,
        title: "Data Science Fundamentals",
        description: "Introduction to data analysis, statistics, and machine learning basics.",
        category: "Data Science",
        difficulty: "intermediate",
        duration: "10 weeks",
        instructor: "Dr. Emily Rodriguez",
        lessons: [
            { id: 1, title: "Introduction to Data Science", duration: "60 minutes" },
            { id: 2, title: "Statistics Basics", duration: "90 minutes" },
            { id: 3, title: "Data Visualization", duration: "75 minutes" },
            { id: 4, title: "Machine Learning Intro", duration: "120 minutes" },
            { id: 5, title: "Practical Applications", duration: "100 minutes" }
        ]
    },
    {
        id: 4,
        title: "Mobile App Development",
        description: "Build native and cross-platform mobile applications using modern frameworks.",
        category: "Mobile Development",
        difficulty: "intermediate",
        duration: "12 weeks",
        instructor: "James Wilson",
        lessons: [
            { id: 1, title: "Mobile Development Overview", duration: "45 minutes" },
            { id: 2, title: "React Native Basics", duration: "90 minutes" },
            { id: 3, title: "UI Components", duration: "85 minutes" },
            { id: 4, title: "Navigation & Routing", duration: "70 minutes" },
            { id: 5, title: "Publishing Apps", duration: "60 minutes" }
        ]
    },
    {
        id: 5,
        title: "Digital Marketing",
        description: "Learn digital marketing strategies including SEO, social media, and content marketing.",
        category: "Marketing",
        difficulty: "beginner",
        duration: "4 weeks",
        instructor: "Lisa Anderson",
        lessons: [
            { id: 1, title: "Marketing Fundamentals", duration: "50 minutes" },
            { id: 2, title: "SEO Strategies", duration: "75 minutes" },
            { id: 3, title: "Social Media Marketing", duration: "80 minutes" },
            { id: 4, title: "Content Creation", duration: "65 minutes" },
            { id: 5, title: "Analytics & Metrics", duration: "70 minutes" }
        ]
    },
    {
        id: 6,
        title: "Cloud Computing",
        description: "Understanding cloud architecture, deployment, and management with AWS and Azure.",
        category: "Cloud Computing",
        difficulty: "advanced",
        duration: "14 weeks",
        instructor: "Robert Taylor",
        lessons: [
            { id: 1, title: "Cloud Computing Basics", duration: "60 minutes" },
            { id: 2, title: "AWS Fundamentals", duration: "120 minutes" },
            { id: 3, title: "Azure Overview", duration: "110 minutes" },
            { id: 4, title: "DevOps Practices", duration: "95 minutes" },
            { id: 5, title: "Security & Compliance", duration: "85 minutes" }
        ]
    }
];

// Quiz questions stored in an array of objects
const quizQuestions = [
    {
        id: 1,
        question: "What does HTML stand for?",
        options: [
            "Hyper Text Markup Language",
            "High Tech Modern Language",
            "Home Tool Markup Language",
            "Hyperlinks and Text Markup Language"
        ],
        correctAnswer: 0
    },
    {
        id: 2,
        question: "Which CSS property is used to change the background color of an element?",
        options: [
            "color",
            "background-color",
            "bg-color",
            "background"
        ],
        correctAnswer: 1
    },
    {
        id: 3,
        question: "What is the correct JavaScript syntax to change the content of an HTML element?",
        options: [
            "document.getElement('p').innerHTML = 'Hello';",
            "document.getElementById('p').innerHTML = 'Hello';",
            "#p.innerHTML = 'Hello';",
            "document.getElementByName('p').innerHTML = 'Hello';"
        ],
        correctAnswer: 1
    },
    {
        id: 4,
        question: "Which HTML5 element is used for navigation links?",
        options: [
            "&lt;navigation&gt;",
            "&lt;nav&gt;",
            "&lt;navbar&gt;",
            "&lt;navigate&gt;"
        ],
        correctAnswer: 1
    },
    {
        id: 5,
        question: "What is the purpose of the CSS Box Model?",
        options: [
            "To create 3D effects on elements",
            "To define how elements are structured and spaced",
            "To organize CSS files",
            "To create animations"
        ],
        correctAnswer: 1
    },
    {
        id: 6,
        question: "Which JavaScript method is used to add an element to the end of an array?",
        options: [
            "push()",
            "pop()",
            "shift()",
            "unshift()"
        ],
        correctAnswer: 0
    },
    {
        id: 7,
        question: "What does responsive web design mean?",
        options: [
            "Websites that respond to user clicks quickly",
            "Websites that adapt to different screen sizes",
            "Websites with interactive animations",
            "Websites that load faster"
        ],
        correctAnswer: 1
    },
    {
        id: 8,
        question: "Which CSS framework is known for its mobile-first approach?",
        options: [
            "Bootstrap",
            "Foundation",
            "Tailwind CSS",
            "All of the above"
        ],
        correctAnswer: 3
    },
    {
        id: 9,
        question: "What is the purpose of localStorage in JavaScript?",
        options: [
            "To store data temporarily during a session",
            "To store data permanently in the browser",
            "To store data on the server",
            "To store CSS styles"
        ],
        correctAnswer: 1
    },
    {
        id: 10,
        question: "Which HTML attribute is used to define inline styles?",
        options: [
            "styles",
            "class",
            "style",
            "css"
        ],
        correctAnswer: 2
    }
];

// Utility functions for grade calculation and score percentage

// Grade calculation logic
function calculateGrade(scorePercentage) {
    if (scorePercentage >= 90) return 'A+';
    else if (scorePercentage >= 80) return 'A';
    else if (scorePercentage >= 70) return 'B';
    else if (scorePercentage >= 60) return 'C';
    else if (scorePercentage >= 50) return 'D';
    else return 'F';
}

// Score percentage calculation
function calculateScorePercentage(correctAnswers, totalQuestions) {
    if (totalQuestions === 0) return 0;
    return (correctAnswers / totalQuestions) * 100;
}

// Pass/Fail determination logic
function determinePassFail(scorePercentage, passingThreshold = 60) {
    return scorePercentage >= passingThreshold;
}

// Performance feedback using switch statement
function getPerformanceFeedback(scorePercentage) {
    switch (true) {
        case (scorePercentage >= 90):
            return "Outstanding! You're a master of this subject!";
        case (scorePercentage >= 80):
            return "Excellent work! You have a strong understanding.";
        case (scorePercentage >= 70):
            return "Good job! Keep practicing to improve further.";
        case (scorePercentage >= 60):
            return "You passed! Review the material to strengthen your knowledge.";
        case (scorePercentage >= 50):
            return "Almost there! A bit more practice will help you pass.";
        default:
            return "Keep studying! Practice makes perfect.";
    }
}

// Web Storage Management
const storageManager = {
    // Save data to localStorage
    saveData: function(key, data) {
        try {
            localStorage.setItem(key, JSON.stringify(data));
            return true;
        } catch (error) {
            console.error('Error saving to localStorage:', error);
            return false;
        }
    },
    
    // Load data from localStorage
    loadData: function(key) {
        try {
            const data = localStorage.getItem(key);
            return data ? JSON.parse(data) : null;
        } catch (error) {
            console.error('Error loading from localStorage:', error);
            return null;
        }
    },
    
    // Remove data from localStorage
    removeData: function(key) {
        try {
            localStorage.removeItem(key);
            return true;
        } catch (error) {
            console.error('Error removing from localStorage:', error);
            return false;
        }
    },
    
    // Clear all localStorage data
    clearAll: function() {
        try {
            localStorage.clear();
            return true;
        } catch (error) {
            console.error('Error clearing localStorage:', error);
            return false;
        }
    }
};

// User data management
const userManager = {
    // Initialize user data if not exists
    initializeUser: function() {
        const userData = storageManager.loadData('userData');
        if (!userData) {
            const defaultUserData = {
                name: 'Student Name',
                email: 'student@example.com',
                memberSince: new Date().toLocaleDateString('en-US', { year: 'numeric', month: 'long' }),
                coursesCompleted: [],
                coursesInProgress: [],
                quizResults: [],
                totalScore: 0,
                streak: 0,
                studyTime: 0
            };
            storageManager.saveData('userData', defaultUserData);
            return defaultUserData;
        }
        return userData;
    },
    
    // Update user data
    updateUser: function(updates) {
        const userData = this.initializeUser();
        Object.assign(userData, updates);
        storageManager.saveData('userData', userData);
        return userData;
    },
    
    // Get user data
    getUser: function() {
        return this.initializeUser();
    }
};

// Course management
const courseManager = {
    // Get all courses
    getAllCourses: function() {
        return courses;
    },
    
    // Get course by ID
    getCourseById: function(courseId) {
        return courses.find(course => course.id === courseId);
    },
    
    // Get courses by difficulty
    getCoursesByDifficulty: function(difficulty) {
        return courses.filter(course => course.difficulty === difficulty);
    },
    
    // Get courses by category
    getCoursesByCategory: function(category) {
        return courses.filter(course => course.category === category);
    },
    
    // Start course for user
    startCourse: function(courseId) {
        const userData = userManager.getUser();
        if (!userData.coursesInProgress.includes(courseId)) {
            userData.coursesInProgress.push(courseId);
            userManager.updateUser({ coursesInProgress: userData.coursesInProgress });
        }
        return userData;
    },
    
    // Complete course for user
    completeCourse: function(courseId) {
        const userData = userManager.getUser();
        
        // Remove from in progress
        const inProgressIndex = userData.coursesInProgress.indexOf(courseId);
        if (inProgressIndex > -1) {
            userData.coursesInProgress.splice(inProgressIndex, 1);
        }
        
        // Add to completed
        if (!userData.coursesCompleted.includes(courseId)) {
            userData.coursesCompleted.push(courseId);
        }
        
        userManager.updateUser(userData);
        return userData;
    }
};

// Quiz management
const quizManager = {
    // Get all quiz questions
    getAllQuestions: function() {
        return quizQuestions;
    },
    
    // Get random questions
    getRandomQuestions: function(count = 5) {
        const shuffled = [...quizQuestions].sort(() => 0.5 - Math.random());
        return shuffled.slice(0, count);
    },
    
    // Calculate quiz results
    calculateResults: function(userAnswers, questions) {
        let correctAnswers = 0;
        const results = [];
        
        questions.forEach((question, index) => {
            const userAnswer = userAnswers[index];
            const isCorrect = userAnswer === question.correctAnswer;
            
            if (isCorrect) {
                correctAnswers++;
            }
            
            results.push({
                question: question.question,
                userAnswer: userAnswer !== null ? question.options[userAnswer] : 'Not answered',
                correctAnswer: question.options[question.correctAnswer],
                isCorrect: isCorrect
            });
        });
        
        const scorePercentage = calculateScorePercentage(correctAnswers, questions.length);
        const grade = calculateGrade(scorePercentage);
        const passed = determinePassFail(scorePercentage);
        const feedback = getPerformanceFeedback(scorePercentage);
        
        return {
            correctAnswers,
            totalQuestions: questions.length,
            scorePercentage,
            grade,
            passed,
            feedback,
            results
        };
    },
    
    // Save quiz results
    saveQuizResults: function(quizResult) {
        const userData = userManager.getUser();
        const quizData = {
            ...quizResult,
            date: new Date().toISOString(),
            timestamp: Date.now()
        };
        
        userData.quizResults.push(quizData);
        userData.totalScore += quizResult.scorePercentage;
        
        if (quizResult.passed) {
            userData.streak += 1;
        } else {
            userData.streak = 0;
        }
        
        userManager.updateUser(userData);
        return userData;
    }
};

// Initialize the application
document.addEventListener('DOMContentLoaded', function() {
    // Initialize user data
    userManager.initializeUser();
    
    // Add smooth scrolling
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth'
                });
            }
        });
    });
    
    // Add loading animation removal
    setTimeout(() => {
        document.querySelectorAll('.loading').forEach(element => {
            element.style.display = 'none';
        });
    }, 1000);
});

// Export for testing
if (typeof module !== 'undefined' && module.exports) {
    module.exports = {
        calculateGrade,
        calculateScorePercentage,
        determinePassFail,
        getPerformanceFeedback,
        courses,
        quizQuestions
    };
}
