define([
    'views/base/view',
    'text!templates/shared/menu.html',
    'text!templates/shared/menu-item.html',
    'models/shared/menu',
    'underscore'
], function(View, template, itemTemplate, menu, _) {
    'use strict';

    return View.extend({
        container: '#menu',
        template: _.template(template),
        itemTemplate: itemTemplate,
        noWrap: true,
        autoRender: true,
        initialize: function() {
            this.listenTo(menu, "change", this.render);
        },
        render: function() {
            var html = this.template({
                items: menu,
                itemTemplate: _.template(this.itemTemplate)
            });

            this.$el.html(html);

            return this;
        }
    });
});