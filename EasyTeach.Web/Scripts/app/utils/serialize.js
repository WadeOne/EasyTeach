define(["underscore"], function(_) {
    "use strict";

    return {
        form: function($form) {
            return _($form.serializeArray())
                .reduce(function(acc, field) {
                    acc[field.name] = field.value;
                    return acc;
                }, {});
        }
    };
});