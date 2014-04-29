define([
	'chaplin'
], function(Chaplin) {
	'use strict';

	var Model = Chaplin.Model.extend({
		// Mixin a synchronization state machine.
		// initialize: function() {
		//   _.extend(this, Chaplin.SyncMachine);
		//   Chaplin.Model.prototype.apply(this, arguments);
		//   this.on('request', this.beginSync);
		//   this.on('sync', this.finishSync);
		//   this.on('error', this.unsync);
		// }

		// TODo: change to sync
		save: function (attributes, options) {
			options.error = function (model, response) {
				switch (response.status) {
					case 401:
						console.log('401!!!!!!!!!!')
						Chaplin.mediator.publish('access-error', response);
						break;
					case 403:
						console.log('403!!!!!!!!!!!', response);
						Chaplin.mediator.publish('auth-error', response);
						break;
					case 404:
						Chaplin.mediator.publish('notfound-error', response);
						break;
					case 405:
						Chaplin.mediator.publish('notallowed-error', response);
						break;
					case 500:
						//TODO: add options to determine behavior (popup or page)
						console.log('500!!!!!!!!!!!', response);
						Chaplin.mediator.publish('server-error', response);
						break;
				}
			}
			return Backbone.Model.prototype.save.call(this, attributes, options);
		}

	});

	return Model;
});
