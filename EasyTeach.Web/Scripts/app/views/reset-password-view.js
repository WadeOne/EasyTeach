define([
    'views/base/view',
    'text!templates/reset-password.html'
], function (View, template) {
    'use strict';

    return View.extend({
        container: '#content',
        template: template,
        autoRender: true        
    });
});
