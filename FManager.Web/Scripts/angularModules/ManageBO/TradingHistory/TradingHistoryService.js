angular.module("FManagerApp").service("TradingHistoryService", ['HttpFactory', function (HttpFactory) {
    this.paginate = function (data) {
        return HttpFactory.post(`/api/TradingHistory/paginate`, data);
    };
}]);