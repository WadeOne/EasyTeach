define([
    'underscore',
    'jquery',
	'chaplin',
	'lib/utils'
], function(_, $, Chaplin, utils) {
	'use strict';

    $(document).ajaxStart(function() {
        $('body').addClass('loader');
    });
    $(document).ajaxComplete(function() {
        $('body').removeClass('loader');
    });

	var defaultErrorHandler = function (model, response) {
		var status = response.status;
		
		switch (status) {
			case 401:
				utils.redirectTo('home#login');
				break;
			case 403:
				utils.redirectTo('error#error', {model:model, status:status, errorMessage: "Oops, access violation"});
				break;
			case 400:
				utils.redirectTo('error#error', {model:model, status:status, errorMessage: "Oops, bad request"});
				break;
			case 404:
				utils.redirectTo('error#error', {model:model, status:status, errorMessage: "Oops, resource not found"});
				break;
			case 405:
			case 500:
				utils.redirectTo('error#error', {model:model, status:status, errorMessage: "Oops, server error"});
				break;
			default:
				utils.redirectTo('error#error', {model:model, status:status, errorMessage: "Oops, unknown error '" + status + "'"});
				break;
		}
	};

	var resolveErrorHandler = function (model, statusCode) {
		var handler = this.modelErrors;

		if (_.isFunction(handler)) {
			return handler;
		} else if (_.isObject(handler) && _.isEmpty(handler) === false) {
			var codeHandler = handler[statusCode.toString()];

			if (_.isFunction(codeHandler)) {
				return codeHandler;
			} else if (_.isFunction(model[codeHandler.toString()])){
                return model[codeHandler.toString()];
            }
		}

		return defaultErrorHandler;
	};

	var errorHandler = function (model, response) {
		resolveErrorHandler
			.call(this, model, response.status)
			.call(this, model, utils.api.getError(response));
	};

	return Chaplin.Model.extend({
		constructor: function () {
			this.on("error", errorHandler, this);

			Chaplin.Model.apply(this, arguments);
		}
	});
});
