define(['views/base/view', 'text!templates/site.html'], function(View, template) {
  'use strict';

  var SiteView = View.extend({
    container: 'body',
    //id: 'site-container',
    regions: {
        main: '#content'
    },
    template: template
  });

  return SiteView;
});
