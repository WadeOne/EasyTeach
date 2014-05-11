define([
	'underscore',
	'models/base/model'
], function (_, Model) {
	'use strict';

	var items = [
		{
			  name: "sample string"
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