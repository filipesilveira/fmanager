angular.module("FManagerApp").service("DayBookItemsService", ['HttpFactory', function (HttpFactory) {
    this.entityShowName = 'DayBookItem';
    this.entityType = 'DayBookItems';
    this.entityPk = 'DayBookItemId';

    this.paginate = function (dayBookId) {
        return HttpFactory.get(`/api/DayBookItems/paginate?dayBookId=${dayBookId}`);
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