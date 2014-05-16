define([
	'underscore',
	'views/base/view',
	'views/quizzes/add-quiz-view',
	'views/quizzes/quiz-list-view',
	'text!templates/quizzes/quiz.html',
	'localStorage'
], function(_, View, AddQuizView, QuizListView, template) {
	'use strict';

	return View.extend({
		container: '#content',
		className: 'row',
		regions: {
			addQuiz: '#add-quiz',
			quizList: '#quiz-list'
		},
		template: template,
		autoRender: true,
		render: function () {
			this.constructor.__super__.render.apply(this, arguments);
			this.subview('addQuiz', new AddQuizView({region: 'addQuiz'}));
			this.subview('quizList', new QuizListView({region: 'quizList'}));
		}
	});
});