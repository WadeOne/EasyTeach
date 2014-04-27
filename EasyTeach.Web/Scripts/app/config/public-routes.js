define(['underscore'], function (_) {
    "use strict";

    return {
        public: ["home#login"],
        login: "home#login",
        isPublic: function (route) {
            return _.contains(this.public, route);
        }
    };
});