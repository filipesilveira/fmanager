angular.module('FManagerApp').controller('DayBooksViewCtrl', ['$scope', 'DayBooksService', 'DayBookItemsService', 'NotifyFactory', '$uibModal', '$routeParams', '$location',
    function ($scope, DayBooksService, DayBookItemsService, NotifyFactory, $uibModal, $routeParams, $location) {

        $scope.initialize = function () {
            $scope.showLoadingIndicator = true;

            if (!$routeParams.id) {
                $scope.showLoadingIndicator = false;
                return;
            }

            $scope.entityId = $routeParams.id;

            DayBooksService.get($scope.entityId)
                .then(function (response) {
                    $scope.DayBookData = response.data;
                    $scope.paginate();
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.edit = function () {
            $scope.editModalInstance = $uibModal.open({
                templateUrl: '/DayBooks/DayBooksEdit/',
                scope: $scope,
                size: 'lg',
                backdrop: 'static',
                keyboard: false
            });

            $scope.editModalInstance.result.then(function () {
                $scope.initialize();
            });
        };

        $scope.delete = function () {
            if (!$scope.entityId) {
                return;
            }

            if (!confirm("Você tem certeza? Isso não pode ser desfeito.")) {
                return;
            }

            $scope.showLoadingIndicator = true;

            DayBooksService.delete($scope.entityId)
                .then(function () {
                    $location.path('/daybooks');
                    $scope.showLoadingIndicator = false;
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.paginate = function () {
            $scope.showLoadingIndicator = true;

            DayBookItemsService.paginate($scope.entityId)
                .then(function (response) {
                    $scope.dayBookItems = response.data;
                    $scope.showLoadingIndicator = false;
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.addItem = function () {
            $scope.editItem();
        };

        $scope.editItem = function (id) {
            $scope.dayBookItemId = id;

            $scope.editItemModalInstance = $uibModal.open({
                templateUrl: '/DayBooks/DayBooksItemEdit/',
                scope: $scope,
                size: 'lg',
                backdrop: 'static',
                keyboard: false
            });

            $scope.editItemModalInstance.result.then(function () {
                $scope.paginate();
                $scope.dayBookItemId = null;
            });
        };

        $scope.deleteItem = function (id) {
            if (!id) {
                return;
            }

            if (!confirm("Você tem certeza? Isso não pode ser desfeito.")) {
                return;
            }

            $scope.showLoadingIndicator = true;

            DayBookItemsService.delete(id)
                .then(function () {
                    $scope.paginate();
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.initialize();
    }]);