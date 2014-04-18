define([
    'controllers/base/controller',
    'views/students/grades-view'
], function(Controller, GradesView) {
    var StudentsController = Controller.extend({
        grades: function () {
            this.view = new GradesView({region: 'main'});
        }
    });

    return StudentsController;
});