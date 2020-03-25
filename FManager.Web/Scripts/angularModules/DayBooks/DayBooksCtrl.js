angular.module("FManagerApp").controller("DayBooksCtrl", ['$scope', 'DayBooksService', 'NotifyFactory',
    function ($scope, DayBooksService, NotifyFactory) {
        $scope.DayBookData = {};
        $scope.showLoadingIndicator = false;

        $scope.initialize = function () {
            if ($scope.entityId) {
                $scope.showLoadingIndicator = true;
                DayBooksService.get($scope.entityId)
                    .then(function (response) {
                        $scope.DayBookData = response.data;
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
            if ($scope.entityId) {
                return DayBooksService.put($scope.entityId, $scope.DayBookData);
            }
            else {
                return DayBooksService.post($scope.DayBookData);
            }
        };

        $scope.closeModal = function () {
            $scope.editModalInstance.close();
        };

        $scope.dismissModal = function () {
            $scope.editModalInstance.dismiss();
        };

        $scope.initialize();
    }]);