// Foundation JavaScript
// Documentation can be found at: http://foundation.zurb.com/docs
$(document).foundation({
	tab: {
      callback : function (tab) {
      }
    },
    accordion: {
	    // specify the class used for active (or open) accordion panels
	    active_class: 'active',
	    // allow multiple accordion panels to be active at the same time
	    multi_expand: false,
	    // allow accordion panels to be closed by clicking on their headers
	    // setting to false only closes accordion panels when another is opened
	    toggleable: true
  	}
});


$( document ).ready(function() {
	 $('#create-user-button').on('click', function(event) {
	 		event.preventDefault();
			var $form = $('#user-registration-form');
			$form.addClass('loader');
			$.ajax({
				url: $form.attr('action'),
				type: 'POST',
				data: $form.serialize(),
				dataType: 'json',
                contentType: 'application/json',
				traditional: true,
				error: function (jqXHR, textStatus, error) {
					$form.removeClass('loader');
					console.log(jqXHR)
				},
				success: function (data) {
					$form.removeClass('loader');
				}
		  });
	 });
	  $('#login-btn').on('click', function(event) {
	 		event.preventDefault();
			var $form = $('#login-form'),
				param_value = "password";
			debugger;
			console.log($form.serialize());
			$form.addClass('loader');
			$.ajax({
				url: $form.attr('action'),
				type: 'POST',
				data: $form.serialize()+ '&grant_type=' + param_value,
				dataType: 'json',
                contentType: 'application/json',
				traditional: true,
				error: function (jqXHR, textStatus, error) {
					$form.removeClass('loader');
					console.log(jqXHR)
				},
				success: function (data) {
					$form.removeClass('loader');
				}
		  });
	 });

});
