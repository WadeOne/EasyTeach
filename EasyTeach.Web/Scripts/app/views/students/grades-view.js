define([
    'views/base/view',
    'text!templates/students/grades.html'
], function(View, template) {
    'use strict';

    return View.extend({
        container: '#content',
        id: 'site-container',
        template: template,
        autoRender: true
    });
});
