define([
	'underscore',
	'views/base/view',
	'models/quizzes/quiz-short',
	'text!templates/quizzes/quiz-description.html',
	'utils/serialize'
], function(_, View, QuizModel, template, serialize) {
	'use strict';

	return View.extend({
		template: template,
		autoRender: true,
		events: {
			//'submit #edit-quiz-form': 'updateQuiz'
			'click #update-quiz': 'updateQuiz'
		},
		render: function () {
			debugger;
			this.model.fetch({
				success: function (response) {
					console.log(response);
				}
			})

			//this.constructor.__super__.render.apply(this, arguments);
		},
		updateQuiz: function (ev) {
			ev.preventDefault()
			var quiz = new QuizModel(),
				form = this.$('#edit-quiz-form');
			quiz.save(serialize.form(form),{
				success: function () {
					console.log('success')
				}
			})
		}
	});
});
