angular.module("FManagerApp").factory('HttpFactory', ['$http', function ($http) {
    var service = {};

    service.get = function (url) {
        return $http.get(url);
    };

    service.post = function (url, data) {
        return $http.post(url, data);
    };

    service.put = function (url, data) {
        return $http.put(url, data);
    };

    service.delete = function (url) {
        return $http.delete(url);
    };

    service.getEntity = function (entityType, id) {
        return service.get('/api/' + entityType + '/' + id);
    };

    service.postEntity = function (entityType, data) {
        return service.post('/api/' + entityType, data);
    };

    service.putEntity = function (entityType, id, data) {
        return service.put('/api/' + entityType + '?id=' + id, data);
    };

    service.deleteEntity = function (entityType, id) {
        return service.delete('/api/' + entityType + '/' + id);
    };

    service.recoverEntity = function (entityType, id) {
        return service.post('/api/' + entityType + '/Recover/' + id);
    };

    return service;
}]);