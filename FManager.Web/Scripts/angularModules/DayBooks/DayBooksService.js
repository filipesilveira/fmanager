angular.module("FManagerApp").service("DayBooksService", ['HttpFactory', function (HttpFactory) {
    this.entityShowName = 'DayBook';
    this.entityType = 'DayBooks';
    this.entityPk = 'DayBookId';

    this.paginate = function () {
        return HttpFactory.get(`/api/DayBooks/paginate`);
    };

    this.get = function (id) {
        return HttpFactory.getEntity(this.entityType, id);
    };

    this.post = function (data) {
        return HttpFactory.postEntity(this.entityType, data);
    };

    this.put = function (id, data) {
        return HttpFactory.putEntity(this.entityType, id, data);
    };

    this.delete = function (id) {
        return HttpFactory.deleteEntity(this.entityType, id);
    };
}]);