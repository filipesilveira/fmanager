angular.module("FManagerApp").service("AccountsService", ["HttpFactory", function (HttpFactory) {
    this.entityShowName = 'Account';
    this.entityType = 'Accounts';
    this.entityPk = 'AccountId';

    this.paginate = function () {
        return HttpFactory.get(`/api/Accounts/paginate`);
    };

    this.getCombobox = function () {
        return HttpFactory.get(`/api/Accounts/combobox`);
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