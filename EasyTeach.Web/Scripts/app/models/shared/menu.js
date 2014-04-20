define([
    'models/base/collection',
    'models/shared/menu-item',
    'backbone',
    'underscore'
], function(Collection, MenuItem, Backbone, _) {
    'use strict';

    var Menu = Collection.extend({
        model: MenuItem,
        initialize: function () {
            var self = this;
            this.subscribeEvent('dispatcher:dispatch', function () {
                var mapped = _.map(self.toJSON(), function(item) {
                    item.active = item.route === "/" + Backbone.history.fragment;
                    return new MenuItem(item);
                });

                self.set(mapped);
                this.trigger("change");
            });
        }
    });

    return new Menu([
        {route: "/register", name: "Студенты"},
        {route: "/students/grades", name: "Таблица с баллами"},
        {route: "./add_homework.html", name: "Список заданий"},
        {route: "./homeworks.html", name: "Выполненные задания"}
    ]);
});