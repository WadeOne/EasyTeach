define([
    'controllers/base/controller',
    'views/errors/error',
    'models/errors/error'
], function (Controller, ErrorView, ErrorModel) {
    "use strict";

    return Controller.extend({
        error: function (params) {
            // todo vic: choose view according to params
            this.view = new ErrorView({ region: 'main', model: new ErrorModel({errorMessage : params.errorMessage, status: params.status }) });
        }
    });
});