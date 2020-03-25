angular.module("FManagerApp").service("ReviewsService", ['HttpFactory', function (HttpFactory) {
    this.paginate = function (data) {
        return HttpFactory.post(`/api/Reviews/paginate`, data);
    };
}]);