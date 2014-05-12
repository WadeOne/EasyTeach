define(function() {
	'use strict';

	// The routes for the application. This module returns a function.
	// `match` is match method of the Router
	return function (match) {
		match('', 'home#login');
		match('register', 'home#register');
		match('error', 'error#error');
		match('reset', 'home#resetPassword');
		//match('confirmEmail/:userId', 'home#confirmEmail', {constraints: {id: /^\d+$/}} );
		match('confirmEmail/:userId/:token', 'home#confirmEmail', {constraints: {userId: /^\d+$/}});
		match('setPassword', 'home#setPassword');
		match('students/grades', 'students#grades');
		match('addhomeworks', 'home#addHomeworks');
		match('quiz', 'quizzes/quiz#showQuizList');
		match('editQuiz', 'quizzes/quiz#editQuiz');
	};
});
