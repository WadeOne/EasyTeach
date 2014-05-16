define([
    'views/base/view',
    'text!templates/list-homeworks.html'
], function (View, template) {
    'use strict';

    return View.extend({
        container: '#content',
        template: template,
        autoRender: true
    });
});