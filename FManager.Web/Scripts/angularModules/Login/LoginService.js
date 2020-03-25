angular.module("FManagerApp").service("LoginService", ['HttpFactory', function (HttpFactory) {

    this.post = function (data) {
        return HttpFactory.post("/Login/Login", data);
    };
}]);