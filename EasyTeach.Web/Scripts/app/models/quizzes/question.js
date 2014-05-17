define([
  'underscore',
  'models/base/model'
], function (_, Model) {
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
			name: ""
		},
		url: "/api/Quiz/Create",
		save: function(item) {
			items.push(item);
			//success callback
			this.trigger('sync');
		},
		fetch: function() {
			return _.each(_.keys(items), function(key) {
				return items[key];
			})
		}
	});
});