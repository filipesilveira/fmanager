angular.module("FManagerApp").service("EntriesService", ["HttpFactory", function (HttpFactory) {
    this.entityShowName = 'Entry';
    this.entityType = 'Entries';
    this.entityPk = 'EntryId';

    this.paginate = function (sectionId) {
        return HttpFactory.get(`/api/Entries/paginate?sectionId=${sectionId}`);
    };

    this.createIQOptionEntries = function (data) {
        return HttpFactory.post(`/api/Entries/CreateIQOptionEntries`, data);
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