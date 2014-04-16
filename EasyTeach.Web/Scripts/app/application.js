define(['chaplin'], function(Chaplin) {
  'use strict';

  var Application = Chaplin.Application.extend({
    title: 'Chaplin Example Application',
    start: function() {
      //var args = [].slice.call(arguments);
      Chaplin.Application.prototype.start.apply(this);
    }
  });

  return Application;
});
