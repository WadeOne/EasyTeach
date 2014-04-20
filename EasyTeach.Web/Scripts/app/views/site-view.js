define(['views/base/view', 'text!templates/shared/site.html'], function(View, template) {
  'use strict';

  var SiteView = View.extend({
    container: 'body',
      //id: 'site-container',    
    regions: {
        main: '#content',
        menu: '#menu'
    },
    template: template,
    events: {
        "click .top-bar .menu-icon": "openMobileMenu"
    },
    openMobileMenu: function () {
        $('.top-bar').toggleClass('expanded');
    }
  });

  return SiteView;
});
