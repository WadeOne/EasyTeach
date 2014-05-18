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
		//url: 'api/Quiz'
		url:  '../api/Quiz'
		//localStorage: new Backbone.LocalStorage('quizzes')
	});
});