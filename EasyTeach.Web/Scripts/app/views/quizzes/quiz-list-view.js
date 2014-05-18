define([
	'underscore',
	'views/base/view',
	'models/quizzes/quiz-list',
	'text!templates/quizzes/quiz-list.html',
	'text!templates/quizzes/quiz-list-item.html'
], function(_, View, QuizListModel, template, itemTemplate) {
	'use strict';

	return View.extend({
		className: 'row',
		itemTemplate: itemTemplate,
		template: _.template(template),
		autoRender: true,
		render: function() {
			var quizzes = new QuizListModel(),
				that = this,
				html;
			quizzes.fetch({
				success: function (quizzes) {
					html = that.template({
						items: quizzes,
						itemTemplate: _.template(that.itemTemplate)

					});
					that.$el.html(html);
				}
			});

			return this;
		}
	});
});
