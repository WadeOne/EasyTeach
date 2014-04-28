define([
    'controllers/base/controller',
    'views/quizzes/quiz-view'
], function(Controller, QuizView) {
    "use strict";

    return Controller.extend({
        showQuiz: function () {
            this.view = new QuizView({region: 'main'});
        }
    });
});