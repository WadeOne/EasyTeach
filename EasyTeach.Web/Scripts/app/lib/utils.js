define([
    'underscore',
    'chaplin'
], function(_, Chaplin) {
    'use strict';

    var api = {
        getError: function(errorData) {
            var parsed = errorData.responseJSON || {};

            var message = parsed.error_description ||
                parsed.error ||
                parsed.message ||
                "неизвестная ошибка";

            return {message: message};
        }
    };

    var utils = Chaplin.utils.beget(Chaplin.utils);

    _(utils).extend({
        api: api
    });

  return utils;
});
