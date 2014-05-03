define([
    'underscore',
    'views/base/view',
    'text!templates/shared/menu.html',
    'text!templates/shared/menu-item.html'
], function(_, View, template, itemTemplate) {
    'use strict';

    return View.extend({
        container: '#menu',
        template: _.template(template),
        itemTemplate: itemTemplate,
        noWrap: true,
        autoRender: true,
        initialize: function() {
            this.listenTo(this.model, "reset", this.render);
        },
        render: function() {
            var html = this.template({
                items: this.model,
                itemTemplate: _.template(this.itemTemplate)
            });

            this.$el.html(html);

            return this;
        }
    });
});