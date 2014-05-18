define([
	'underscore',
	'models/base/model',
	'localStorage'
], function (_, Model) {
	'use strict';

	return Backbone.Model.extend({
		defaults: {
			//name: "",
			//description: "",
			//questions: [
			//]
		},
		//url: 'api/Quiz'
		url:  '../api/Quiz'
		/*initialize: function () {
			this.on('add', this.addHandler);
			this.on('change', this.changeHandler);
			this.on('remove', this.removeHandler);
		},
		addHandler: function () {
			this.save();
		},
		changeHandler: function () {
			this.save(this.changed);
		},
		removeHandler: function () {
			this.destroy();
		},
		localStorage: new Backbone.LocalStorage('quizzes')*//*,
		url: "/api/Quiz/Create"*/
		/*save: function(item) {
			items.push(item);
			//success callback
			this.trigger('sync');
		},
		fetch: function(options) {
			this.trigger('sync');
			return _.each(_.keys(items), function(key) {
				return items[key];
			})
		}*/
	});
});