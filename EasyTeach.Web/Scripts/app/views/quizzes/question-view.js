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
		container: '#questions-container',
		className: 'question-wrap',
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
			var type = $(ev.currentTarget).val();
			switch(type) {
				case 'Text':
					this.$el.find('.answer-helper div').remove('');
					this.$el.find('.add-option-btn').hide();
					new AnswerText({template: textTemplate, container: this.$el.find('.answer-helper')});
					break;
				case 'Select':
					this.$el.find('.answer-helper div').remove('');
		     		this.currentTpl = radioTemplate;
		     		this.currentContainer = this.$el.find('.answer-helper');
					new AnswerText({template: this.currentTpl, container: this.$el.find('.answer-helper')});
		     		this.$el.find('.add-option-btn').show();
					break;
				case 'MultiSelect':
		     		this.$el.find('.answer-helper div').remove();
					this.currentTpl = checkTemplate;
					this.currentContainer = this.$el.find('.answer-helper');
					new AnswerText({template: this.currentTpl, container: this.$el.find('.answer-helper')});
					this.$el.find('.add-option-btn').show();
					break;
			}
		},
		addOption: function (ev) {
			new AnswerText({template: this.currentTpl, container: this.currentContainer});
		}

	});
});
