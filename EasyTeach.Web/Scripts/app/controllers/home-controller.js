
define([
		'controllers/base/controller',
		'views/login-view',
		'views/register-view'
	], function(Controller, LoginView, RegisterView){
		var HomeController = Controller.extend({
			login: function(params) {
				this.view = new LoginView();
			}
		})
		return HomeController;

})