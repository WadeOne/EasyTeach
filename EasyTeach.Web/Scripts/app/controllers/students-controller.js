define([
    'controllers/base/controller',
    'views/students/grades-view'
], function(Controller, GradesView) {
    "use strict";

    return Controller.extend({
        grades: function () {
            this.view = new GradesView({region: 'main'});
        }
    });
});