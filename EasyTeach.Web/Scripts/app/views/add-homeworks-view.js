define([
    'views/base/view',
    'text!templates/add-homeworks.html'
], function (View, template) {
    'use strict';

    return View.extend({
        container: '#content',
        template: template,
        autoRender: true
    });
});