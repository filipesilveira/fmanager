angular.module("FManagerApp").service("AssertivenessService", ['HttpFactory', function (HttpFactory) {
    this.paginate = function (data) {
        return HttpFactory.post(`/api/Assertiveness/paginate`, data);
    };
}]);