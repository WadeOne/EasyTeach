requirejs.config({
    baseUrl: '/Scripts/app',
    paths: {
        jquery: '../vendor/bower_components/jquery/dist/jquery',
        'foundation.core': '../vendor/bower_components/foundation/js/foundation',
        underscore: '../vendor/bower_components/lodash/dist/lodash',
        backbone: '../vendor/bower_components/backbone/backbone',
        handlebars: '../vendor/bower_components/handlebars/handlebars',
        text: '../vendor/bower_components/requirejs-text/text',
        chaplin: '../vendor/bower_components/chaplin/chaplin',
        modernizr: '../vendor/bower_components/modernizr/modernizr'
    },
    shim: {
        'foundation.core': {
            deps: [
                'jquery',
                'modernizr'
            ],
            exports: 'Foundation'
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
        },
        'modernizr': {
            exports: 'Modernizr'
        }
    }
});

// Bootstrap the application
require(['application', 'routes'], function (Application, routes) {
    new Application({ routes: routes, controllerSuffix: '-controller' });
});

require(['jquery', 'foundation.core'], function ($) {
    $(document).foundation({
    });
});