define([
	'underscore',
	'views/base/view',
	'models/quizzes/quiz-short',
	'models/quizzes/quiz-list',
	'text!templates/quizzes/add-quiz.html',
	'localStorage'
], function(_, View, QuizModel, QuizList, template) {
	'use strict';

	return View.extend({
		className: 'row',
		template: template,
		autoRender: true,
		events: {
			'submit #add-quiz-form': "createQuiz"
		},
		initialize: function() {
			this.model = new QuizModel();
			//this.listenTo(this.model, "sync", this.registerSuccess);
		},
		createQuiz: function (ev) {

			var quizzes = new QuizList();
			var form = $(ev.currentTarget),
			userInfo = {
				name: form.find('input[name=title]').val()
			};
			quizzes.create(userInfo);

			//this.model.save(userInfo);
			return false;
		},
		registerSuccess: function () {
			console.log(arguments);
		}
	});
});
