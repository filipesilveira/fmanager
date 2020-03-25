angular.module("FManagerApp").service("ParitiesService", ["HttpFactory", function (HttpFactory) {
    this.entityShowName = 'Parity';
    this.entityType = 'Parities';
    this.entityPk = 'ParityId';

    this.paginate = function () {
        return HttpFactory.get(`/api/Parities/paginate`);
    };
}]);