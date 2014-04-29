define([
    'views/base/view',
    'text!templates/quizzes/quiz.html'
], function(View, template) {
    'use strict';

    return View.extend({
        container: '#content',
        className: 'row',
        template: template,
        autoRender: true
    });
});
