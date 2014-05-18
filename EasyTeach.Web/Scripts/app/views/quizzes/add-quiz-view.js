define([
	'underscore',
	'views/base/view',
	'models/quizzes/quiz-short',
	'models/quizzes/quiz-list',
	'text!templates/quizzes/add-quiz.html',
	'lib/utils'
], function(_, View, QuizModel, QuizList, template, utils) {
	'use strict';

	return View.extend({
		className: 'row',
		template: template,
		autoRender: true,
		events: {
			'submit #add-quiz-form': "createQuiz"
		},
		createQuiz: function (ev) {
			//var quizzes = new QuizList();
			var form = $(ev.currentTarget),
				userInfo = {
					name: form.find('input[name=title]').val()
				},
				quiz = new QuizModel();
			//quizzes.create(userInfo);

			quiz.save(userInfo, {
				success: function (model, response, options) {
					utils.redirectTo({url: '/quiz/' + response.quizId})
				}
			});
			return false;
		}
	});
});
