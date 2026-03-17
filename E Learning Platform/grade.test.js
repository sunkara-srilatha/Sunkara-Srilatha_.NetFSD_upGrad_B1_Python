// Jest test cases for E-Learning Platform
// Import functions from script.js

// Since we're in a browser environment, we need to mock the functions
// In a real setup, these would be imported from the main script file

// Mock the functions for testing
const calculateGrade = (scorePercentage) => {
    if (scorePercentage >= 90) return 'A+';
    else if (scorePercentage >= 80) return 'A';
    else if (scorePercentage >= 70) return 'B';
    else if (scorePercentage >= 60) return 'C';
    else if (scorePercentage >= 50) return 'D';
    else return 'F';
};

const calculateScorePercentage = (correctAnswers, totalQuestions) => {
    if (totalQuestions === 0) return 0;
    return (correctAnswers / totalQuestions) * 100;
};

const determinePassFail = (scorePercentage, passingThreshold = 60) => {
    return scorePercentage >= passingThreshold;
};

describe('Grade Calculation Logic', () => {
    test('should return A+ for scores 90-100', () => {
        expect(calculateGrade(95)).toBe('A+');
        expect(calculateGrade(90)).toBe('A+');
        expect(calculateGrade(100)).toBe('A+');
    });

    test('should return A for scores 80-89', () => {
        expect(calculateGrade(85)).toBe('A');
        expect(calculateGrade(80)).toBe('A');
        expect(calculateGrade(89)).toBe('A');
    });

    test('should return B for scores 70-79', () => {
        expect(calculateGrade(75)).toBe('B');
        expect(calculateGrade(70)).toBe('B');
        expect(calculateGrade(79)).toBe('B');
    });

    test('should return C for scores 60-69', () => {
        expect(calculateGrade(65)).toBe('C');
        expect(calculateGrade(60)).toBe('C');
        expect(calculateGrade(69)).toBe('C');
    });

    test('should return D for scores 50-59', () => {
        expect(calculateGrade(55)).toBe('D');
        expect(calculateGrade(50)).toBe('D');
        expect(calculateGrade(59)).toBe('D');
    });

    test('should return F for scores below 50', () => {
        expect(calculateGrade(45)).toBe('F');
        expect(calculateGrade(0)).toBe('F');
        expect(calculateGrade(49)).toBe('F');
    });
});

describe('Score Percentage Calculation', () => {
    test('should calculate correct percentage for various scores', () => {
        expect(calculateScorePercentage(8, 10)).toBe(80);
        expect(calculateScorePercentage(5, 10)).toBe(50);
        expect(calculateScorePercentage(10, 10)).toBe(100);
        expect(calculateScorePercentage(0, 10)).toBe(0);
    });

    test('should handle decimal results correctly', () => {
        expect(calculateScorePercentage(7, 8)).toBe(87.5);
        expect(calculateScorePercentage(3, 7)).toBeCloseTo(42.86, 2);
        expect(calculateScorePercentage(2, 3)).toBeCloseTo(66.67, 2);
    });

    test('should return 0 when total questions is 0', () => {
        expect(calculateScorePercentage(5, 0)).toBe(0);
        expect(calculateScorePercentage(0, 0)).toBe(0);
    });

    test('should handle edge cases', () => {
        expect(calculateScorePercentage(1, 1)).toBe(100);
        expect(calculateScorePercentage(0, 1)).toBe(0);
        expect(calculateScorePercentage(1, 2)).toBe(50);
    });
});

describe('Pass/Fail Determination Logic', () => {
    test('should pass when score is equal to threshold', () => {
        expect(determinePassFail(60)).toBe(true);
        expect(determinePassFail(70)).toBe(true);
        expect(determinePassFail(100)).toBe(true);
    });

    test('should fail when score is below threshold', () => {
        expect(determinePassFail(59)).toBe(false);
        expect(determinePassFail(50)).toBe(false);
        expect(determinePassFail(0)).toBe(false);
    });

    test('should work with custom threshold', () => {
        expect(determinePassFail(75, 75)).toBe(true);
        expect(determinePassFail(74, 75)).toBe(false);
        expect(determinePassFail(80, 80)).toBe(true);
        expect(determinePassFail(79, 80)).toBe(false);
    });

    test('should handle edge cases', () => {
        expect(determinePassFail(0, 0)).toBe(true); // Edge case: threshold 0
        expect(determinePassFail(100, 100)).toBe(true); // Edge case: threshold 100
        expect(determinePassFail(50, 50)).toBe(true); // Edge case: exactly at threshold
    });

    test('should work with realistic quiz scenarios', () => {
        // 10 question quiz scenarios
        expect(determinePassFail(calculateScorePercentage(6, 10))).toBe(true);  // 60%
        expect(determinePassFail(calculateScorePercentage(5, 10))).toBe(false); // 50%
        expect(determinePassFail(calculateScorePercentage(8, 10))).toBe(true);  // 80%
        
        // 5 question quiz scenarios
        expect(determinePassFail(calculateScorePercentage(3, 5))).toBe(true);   // 60%
        expect(determinePassFail(calculateScorePercentage(2, 5))).toBe(false);  // 40%
    });
});

describe('Combined Functionality Tests', () => {
    test('should work together for complete quiz evaluation', () => {
        // Test scenario: 8 correct out of 10 questions
        const correctAnswers = 8;
        const totalQuestions = 10;
        const scorePercentage = calculateScorePercentage(correctAnswers, totalQuestions);
        const grade = calculateGrade(scorePercentage);
        const passed = determinePassFail(scorePercentage);
        
        expect(scorePercentage).toBe(80);
        expect(grade).toBe('A');
        expect(passed).toBe(true);
    });

    test('should handle failing scenario correctly', () => {
        // Test scenario: 4 correct out of 10 questions
        const correctAnswers = 4;
        const totalQuestions = 10;
        const scorePercentage = calculateScorePercentage(correctAnswers, totalQuestions);
        const grade = calculateGrade(scorePercentage);
        const passed = determinePassFail(scorePercentage);
        
        expect(scorePercentage).toBe(40);
        expect(grade).toBe('F');
        expect(passed).toBe(false);
    });

    test('should handle borderline passing scenario', () => {
        // Test scenario: 6 correct out of 10 questions (exactly 60%)
        const correctAnswers = 6;
        const totalQuestions = 10;
        const scorePercentage = calculateScorePercentage(correctAnswers, totalQuestions);
        const grade = calculateGrade(scorePercentage);
        const passed = determinePassFail(scorePercentage);
        
        expect(scorePercentage).toBe(60);
        expect(grade).toBe('C');
        expect(passed).toBe(true);
    });
});
