requirejs.config({
    baseUrl: '/Scripts/app',
    paths: {
        jquery: '../vendor/bower_components/jquery/dist/jquery',
        fastClick: '../vendor/bower_components/fastclick/lib/fastclick',
        'foundation.core': '../vendor/bower_components/foundation/js/foundation',
        'foundation.topbar': '../vendor/bower_components/foundation/js/foundation/foundation.topbar',
        underscore: '../vendor/bower_components/lodash/dist/lodash',
        backbone: '../vendor/bower_components/backbone/backbone',
        handlebars: '../vendor/bower_components/handlebars/handlebars',
        text: '../vendor/bower_components/requirejs-text/text',
        chaplin: '../vendor/bower_components/chaplin/chaplin',
        modernizr: '../vendor/bower_components/modernizr/modernizr',
        localStorage: '../vendor/backbone.localStorage'
    },
    shim: {
        'modernizr': {
            exports: 'Modernizr'
        },
        'foundation.core': {
            deps: [
                'jquery',
                'modernizr',
                'fastClick'
            ],
            exports: 'Foundation'
        },
        'foundation.topbar': {
            deps: [
                'jquery',
                'modernizr',
                'foundation.core'
            ],
            exports: 'FoundationTopBar'
        },
        localStorage: {
            deps: [
                'backbone'
            ]
        },
        underscore: {
            exports: '_'
        },
        backbone: {
            deps: ['underscore', 'jquery'],
            exports: 'Backbone'
        },
        handlebars: {
            exports: 'Handlebars'
        }
    }
});

// Bootstrap the application
require(['application', 'routes'], function (Application, routes) {
    new Application({ routes: routes, controllerSuffix: '-controller' });
});

require(['jquery', 'foundation.core', 'foundation.topbar'], function ($) {
    $(document).foundation({
    });
});