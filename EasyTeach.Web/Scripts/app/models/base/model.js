define([
    'underscore',
	'chaplin'
], function(_, Chaplin) {
	'use strict';

    var defaultErrorHandler = function (model, response) {
        var status = response.status;

        switch (status) {
            case 401:
                Chaplin.utils.redirectTo('home#login');
                break;
            case 403:
                Chaplin.utils.redirectTo('error#error', {model:model, errorMessage: "Oops, access violation"});
                break;
            case 400:
                Chaplin.utils.redirectTo('error#error', {model:model, errorMessage: "Oops, bad request"});
                break;
            case 404:
                Chaplin.utils.redirectTo('error#error', {model:model, errorMessage: "Oops, resource not found"});
                break;
            case 405:
            case 500:
                Chaplin.utils.redirectTo('error#error', {model:model, errorMessage: "Oops, server error"});
                break;
            default:
                Chaplin.utils.redirectTo('error#error', {model:model, errorMessage: "Oops, unknown error '" + status + "'"});
                break;
        }
    };

    var resolveErrorHandler = function (statusCode) {
        var handler = this.modelErrors;

        if (_.isFunction(handler)) {
            return handler;
        } else if (_.isObject(handler) && _.isEmpty(handler) === false) {
            var codeHandler = handler[statusCode.toString()];

            if (_.isFunction(codeHandler)) {
                return codeHandler;
            }
        }

        return defaultErrorHandler;
    };

    var errorHandler = function (model, response) {
        resolveErrorHandler
            .call(this, response.status)
            .apply(this, arguments);
    };

	return Chaplin.Model.extend({
        constructor: function () {
            this.on("error", errorHandler, this);

            Chaplin.Model.apply(this, arguments);
        }
	});
});
