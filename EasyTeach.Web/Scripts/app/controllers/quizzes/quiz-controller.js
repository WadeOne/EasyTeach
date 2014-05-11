define([
    'controllers/base/controller',
    'views/quizzes/quiz-list-view'
], function(Controller, QuizView) {
    "use strict";

    return Controller.extend({
        showQuizList: function () {
            this.view = new QuizView({region: 'main'});
        }
    });
});