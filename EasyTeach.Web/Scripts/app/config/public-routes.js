define(['underscore'], function (_) {
    "use strict";

    return {
        public: ["home#login", "home#resetPassword"],
        login: "home#login",
        loginRedirect: "students#grades",
        isPublic: function (route) {
            return _.contains(this.public, route);
        }
    };
});