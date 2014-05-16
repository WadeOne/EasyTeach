define([
    'models/base/model'
], function (Model) {
    'use strict';

    return Model.extend({
        url: '../api/User/ConfirmEmail'
    });
});