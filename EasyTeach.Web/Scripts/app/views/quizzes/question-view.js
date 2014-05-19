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
		container: '#question-wrap',
		events: {
			'click .update-question-btn': 'updateQuestion',
			'click input[type=radio]': 'chooseType',
			'click .add-option-btn': 'addOption'
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
					console.log('success');
				}
			})
		},
		chooseType: function (ev) {
			var type = $(ev.currentTarget).val()
			switch(type) {
				case 'Text':
					this.$el.find('.answer-container div').remove('');
					this.subview('answer', new AnswerText({region: 'answer', template: textTemplate, multiple: false}));
					this.$el.parent().find('.add-option-btn').hide();
					break;
				case 'Select':
					this.$el.find('.answer-container div').remove('');
					this.subview('answer', new AnswerText({region: 'answer', template: radioTemplate, multiple: true}));
		     		this.$el.parent().find('.add-option-btn').show();
		     		this.currentTpl = radioTemplate;
					break;
				case 'MultiSelect':
		     		this.$el.find('.answer-container div').remove();
					this.subview('answer', new AnswerText({region: 'answer', template: checkTemplate, multiple: true}));
					this.$el.parent().find('.add-option-btn').show();
					this.currentTpl = checkTemplate;
					break;
			}
		},
		addOption: function (ev) {
			var flag = this.currentTpl
			new AnswerText({region: 'answer', template: this.currentTpl});
		}

	});
});
