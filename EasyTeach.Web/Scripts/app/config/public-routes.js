define(['underscore'], function (_) {
    "use strict";

    return {
        public: ["home#login", "home#resetPassword", "error#error"],
        login: "home#login",
        loginRedirect: "students#grades",
        isPublic: function (route) {
            return _.contains(this.public, route);
        }
    };
});