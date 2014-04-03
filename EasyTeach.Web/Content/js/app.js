// Foundation JavaScript
// Documentation can be found at: http://foundation.zurb.com/docs
//$(document).foundation();

$( document ).ready(function() {
	 $('#create-user-button').on('click', function(event) {
		  var $form = $('#user-registration-form');
		  $.ajax({
		    url: $form.attr('action'),
		    type: 'POST',
		    data: $form.serialize(),
		    traditional: true,
		    error: function (jqXHR, textStatus, error) {
		     
		    },
		    success: function (data) {
		    }
		  });
	 })
});
