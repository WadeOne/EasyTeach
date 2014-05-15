define([
	'controllers/base/controller',
	'views/quizzes/quiz-view',
	'views/quizzes/edit-quiz-view'
], function(Controller, QuizView, EditQuizView) {
	"use strict";

	return Controller.extend({
		showQuizList: function () {
			this.view = new QuizView({region: 'main'});
		},
		editQuiz: function () {
			this.view = new EditQuizView({region: 'main'});
		}
	});
});