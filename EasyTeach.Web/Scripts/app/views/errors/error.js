define([
    'chaplin',
    'views/base/view',
    'models/user-login',
    'text!templates/login.html',
    'utils/serialize'
], function (Chaplin, View, UserLogin, template, serialize) {
    'use strict';

    return View.extend({
        container: '#content',
        id: 'site-container', 
        template: template,
        autoRender: true,
        noWrap: true
    });
});
