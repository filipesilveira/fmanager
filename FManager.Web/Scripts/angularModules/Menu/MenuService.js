angular.module("FManagerApp").service("MenuService", ["HttpFactory", function (HttpFactory) {
    this.entityShowName = 'Menu';
    this.entityType = 'Menu';
    this.entityPk = 'MenuId';

    this.get = function () {
        return HttpFactory.get(`/api/Menu/`);
    };
}]);