define([
    'views/base/view',
    'text!templates/students/grades.html'
], function(View, template) {
    'use strict';

    var GradesView = View.extend({
        container: '#content',
        id: 'site-container',
        template: template,
        autoRender: true
    });

    return GradesView;
});
