define([
	'underscore',
	'backbone',
	'models/base/collection',
	'models/quizzes/quiz-short',
	'config/menu',
	'localStorage'
], function(_, Backbone, Collection, QuizListItem, config) {
	'use strict';

	return Collection.extend({
		model: QuizListItem,
		localStorage: new Backbone.LocalStorage('quizzes')
	});
});