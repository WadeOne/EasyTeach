define([
	'controllers/base/controller',
	//'views/quizzes/add-quiz-view'
	//'views/quizzes/quiz-list-view'
	'views/quizzes/quiz-view'
], function(Controller, QuizView) {
	"use strict";

	return Controller.extend({
		showQuizList: function () {
			this.view = new QuizView({region: 'main'});
		}
	});
});