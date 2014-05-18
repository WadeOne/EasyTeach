define([
	'underscore',
	'views/base/view',
	'views/quizzes/answer-text-view',
	'models/quizzes/question',
	'text!templates/quizzes/question.html',

	'text!templates/quizzes/answer-text.html',
	'text!templates/quizzes/answer-checkbox.html',
	'text!templates/quizzes/answer-radio.html',
	'utils/serialize'
], function(_, View, AnswerText, QuizModel, template, textTemplate, checkTemplate, radioTemplate, serialize) {
	'use strict';

	return View.extend({
		template: template,
		autoRender: true,
		container: '#question-list',
		events: {
			'click .update-question-btn': 'updateQuestion',
			'click input[type=radio]': 'chooseType',
		},
		regions: {
			'answer': '.answer-container'
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
		},
		chooseType: function (ev) {
			debugger;
			var type = $(ev.currentTarget).val()
			switch(type) {
				case 'Text':
					this.subview('answer', new AnswerText({region: 'answer', template: textTemplate, multiple: false}));
					break;
				case 'Select':
					this.subview('answer', new AnswerText({region: 'answer', template: radioTemplate, multiple: true}));
					break;
				case 'MultiSelect':
					this.subview('answer', new AnswerText({region: 'answer', template: checkTemplate, multiple: true}));
					break;
			}
			console.log($(ev.currentTarget).val())
		}

	});
});
