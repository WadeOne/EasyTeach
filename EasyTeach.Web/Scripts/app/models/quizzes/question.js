define([
	'underscore',
	'models/base/model',
	'models/quizzes/quiz-short',
	'localStorage'
], function (_, Model, QuizModel) {
	'use strict';

	var items = [
		{
			"quizId": 1,
			"question": {
				"questionText": "sample string 1",
				"questionType": "MultiSelect",
				"questionItems": [
					{
						"text": "sample string 1",
						"isSolution": true
					},
					{
						"text": "sample string 1",
						"isSolution": true
					}
				]
			}
		}
	];

	return Model.extend({
		defaults: {
			"quizId": 0,
			"question": {
				"questionText": "",
				"questionType": "Text",
				"questionItems": []
			}
		},
		url: '../api/Quiz/AddQuestion'
		
		/*initialize: function () {
			this.on('add', this.addHandler);
			this.on('change', this.changeHandler);
			this.on('remove', this.removeHandler);
			this.quizModel = new QuizModel();
		},
		addHandler: function (model) {
			debugger;
			var data = model.attributes;
			this.quizModel.save();
		},
		changeHandler: function (model) {
			var question = model.attributes,
				formData,
				self = this;
				var c = new Backbone.Collection();
					c.localStorage = new Backbone.LocalStorage("quizzes");
					c.fetch();
					console.log(c.pluck('id'));
					debugger;
				self.quizModel.set({id: question.quizId}).fetch({
					success: function (model, data) {
						debugger;
						data.questions.push(question);
						self.quizModel.save(data);
					}
				});
		},
		removeHandler: function () {
			this.destroy();
		},
		localStorage: new Backbone.LocalStorage('quizzes')*/
	});
});