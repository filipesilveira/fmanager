angular.module("FManagerApp").service("SectionsService", ["HttpFactory", function (HttpFactory) {
    this.entityShowName = 'Section';
    this.entityType = 'Sections';
    this.entityPk = 'SectionId';

    this.paginate = function (params, accountId) {
        return HttpFactory.get(`/api/Sections/paginate?accountId=${accountId}&page=${params.page()}&count=${params.count()}`);
    };

    this.createIQOptionSections = function (data) {
        return HttpFactory.post(`/api/Sections/CreateIQOptionSections`, data);
    };

    this.get = function (id, accountId) {
        return HttpFactory.get(`/api/Sections?id=${id}&accountId=${accountId}`);
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