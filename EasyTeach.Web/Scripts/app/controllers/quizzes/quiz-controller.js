define([
	'controllers/base/controller',
	'models/quizzes/quiz-short',
	'views/quizzes/quiz-view',
	'views/quizzes/edit-quiz-view'
], function(Controller, QuizModel, QuizView, EditQuizView) {
	"use strict";

	return Controller.extend({
		showQuizList: function () {
			this.view = new QuizView({region: 'main'});
		},
		editQuiz: function (params) {
			this.view = new EditQuizView({region: 'main', model: new QuizModel({id: params.id})});
		}
	});
});