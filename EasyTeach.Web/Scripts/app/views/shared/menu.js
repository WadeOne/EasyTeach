define([
    'views/base/view',
    'text!templates/shared/menu.html'
], function(View, template) {
    'use strict';

    var MenuView = View.extend({
        container: '#menu',
        tagName: 'ul',
        className: 'left',
        template: template,
        autoRender: true,
        render: function() {
            this.$el.html(this.template);
            var activeClass = "active";

            var $el = this.$el;

            var activate = function () {
                $el.find("li").removeClass(activeClass);

                $(this).addClass(activeClass);
            };

            $el.find("li").on('click', activate);

            $el.find("li a[href='/" + Backbone.history.fragment + "']")
                .closest("li")
                .click();

            return this;
        }
    });

    return MenuView;
});