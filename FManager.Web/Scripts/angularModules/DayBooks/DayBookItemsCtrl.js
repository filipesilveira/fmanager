angular.module("FManagerApp").controller("DayBookItemsCtrl", ['$scope', 'DayBookItemsService', 'NotifyFactory',
    function ($scope, DayBookItemsService, NotifyFactory) {
        $scope.DayBookItemData = { DayBookId: $scope.entityId };
        $scope.showLoadingIndicator = false;

        $scope.initialize = function () {
            if ($scope.dayBookItemId) {
                $scope.showLoadingIndicator = true;
                DayBookItemsService.get($scope.dayBookItemId)
                    .then(function (response) {
                        $scope.DayBookItemData = response.data;
                        $scope.showLoadingIndicator = false;
                    })
                    .catch(function () {
                        NotifyFactory.error();
                    });
            }
        };

        $scope.save = function () {
            $scope.showLoadingIndicator = true;
            $scope.getSavePromise()
                .then(() => {
                    NotifyFactory.success();
                    $scope.closeModal();
                    $scope.showLoadingIndicator = false;
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.getSavePromise = function () {
            if ($scope.dayBookItemId) {
                return DayBookItemsService.put($scope.dayBookItemId, $scope.DayBookItemData);
            }
            else {
                return DayBookItemsService.post($scope.DayBookItemData);
            }
        };

        $scope.closeModal = function () {
            $scope.editItemModalInstance.close();
        };

        $scope.dismissModal = function () {
            $scope.editItemModalInstance.dismiss();
        };

        $scope.initialize();
    }]);