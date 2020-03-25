angular.module("FManagerApp").service("CurrenciesService", ["HttpFactory", function (HttpFactory) {
    this.entityShowName = 'Currency';
    this.entityType = 'Currencies';
    this.entityPk = 'CurrencyId';

    this.paginate = function () {
        return HttpFactory.get(`/api/Currencies/paginate`);
    };
}]);