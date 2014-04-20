define([
    'underscore',
    'backbone',
    'models/base/collection',
    'models/shared/menu-item',
    'config/menu'
], function(_, Backbone, Collection, MenuItem, config) {
    'use strict';

    return Collection.extend({
        model: MenuItem,
        initialize: function() {
            this.subscribeEvent('dispatcher:dispatch', this.fetch);
        },
        fetch: function() {
            var mapped = _.map(config, function(item) {
                item.active = item.route === "/" + Backbone.history.fragment;
                return new MenuItem(item);
            });

            this.set(mapped);
            this.trigger("change");
        }
    });
});