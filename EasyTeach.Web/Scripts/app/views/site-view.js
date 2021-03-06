define([
    'views/base/view',
    'text!templates/shared/site.html'
], function(View, template) {
  'use strict';

  return View.extend({
    container: 'body',
    regions: {
        main: '#content',
        menu: '#menu'
    },
    template: template
  });
});
