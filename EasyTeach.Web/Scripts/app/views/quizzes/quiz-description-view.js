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
			var self = this;
			this.model.fetch({
				success: function (response) {
					var html = _.template(self.template);
					self.$el.html(html(response.attributes));
				}
			})

		},
		updateQuiz: function (ev) {
			ev.preventDefault()
			var quiz = new QuizModel(),
				form = this.$('#edit-quiz-form');
			quiz.save(serialize.form(form),{
				success: function () {
					/*var html = _.template(template, {}),
					console.log('success')*/
				}
			})
		}
	});
});
