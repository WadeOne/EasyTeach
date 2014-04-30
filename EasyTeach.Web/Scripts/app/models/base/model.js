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
		initialize: function () {
			this.on("error", this.errorHandler);
		},

		errorHandler: function (model, response) {
			switch (response.status) {
				case 401:
					this.accessErrorHandler(response);
					break;
				case 403:
					this.authErrorHandler(response);
					break;
				case 404:
					this.errorPageRedirect(response);
					break;
				case 405:
					console.log('not allowed');
					break;
				case 500:
					//TODO: add options to determine behavior (popup or page)
					this.errorPageRedirect(response);
					break;
			}
		},
		errorPageRedirect: function (response) {
			window.localStorage.setItem('errorInfo', JSON.stringify(response));
			Chaplin.utils.redirectTo('home#error');
		},
		accessErrorHandler: function (response) {
			Chaplin.utils.redirectTo('home#login');
		},
		authErrorHandler: function (response) {
			Chaplin.utils.redirectTo('home#login');
		}

	});

	return Model;
});
