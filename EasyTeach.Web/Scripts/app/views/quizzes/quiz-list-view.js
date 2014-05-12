define([
	'views/base/view',
	'models/quizzes/quiz-short',
	'text!templates/quizzes/quiz-list.html'
], function(View, QuizzShortModel, template) {
	'use strict';

	return View.extend({
		container: '#content',
		className: 'row',
		template: template,
		autoRender: true,
		events: {
			'submit #add-quiz-form': "createQuiz"
		},
		initialize: function() {
			this.model = new QuizzShortModel();
			this.listenTo(this.model, "sync", this.registerSuccess);
			this.listenTo(this.model, "error", this.render);
		},
		createQuiz: function (ev) {
			var form = $(ev.currentTarget),
			userInfo = {
				name: form.find('input[name=title]').val()
			};

			this.model.save(userInfo);
			return false;
		},
		registerSuccess: function () {
			//utils.redirectTo('quizzes/quiz#editQuiz');
		}
	});
});
