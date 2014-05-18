define([
	'underscore',
	'views/base/view',
	'models/quizzes/question',
	'text!templates/quizzes/question.html',
	'utils/serialize'
], function(_, View, QuizModel, template, serialize) {
	'use strict';

	return View.extend({
		template: template,
		autoRender: true,
		container: '#question-list',
		events: {
			'click .update-question-btn': 'updateQuestion'
		},
		render: function () {
			this.$el.append(template);
		},
		updateQuestion: function (ev) {
			ev.preventDefault();
			var quiz = new QuizModel(),
				form = $(ev.currentTarget).parent(),
				data = {
					quizId: $('input[name=id]').val(),
					question: {
						questionText: form.find('[name=questionText]').val(),
						questionType: form.find('[name=questionType]').val(),
						questionItems: [
							{
								text: form.find('.answer').eq(0).val(),
								isSolution: true
							}
						]
					}
				}
			quiz.save(data, {
				success: function () {
					debugger;
				}
			})
		}

	});
});
