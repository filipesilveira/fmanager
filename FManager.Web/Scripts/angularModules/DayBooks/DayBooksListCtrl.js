angular.module('FManagerApp').controller('DayBooksListCtrl', ['$scope', 'DayBooksService', 'NotifyFactory', '$uibModal',
    function ($scope, DayBooksService, NotifyFactory, $uibModal) {

        $scope.paginate = function () {
            $scope.showLoadingIndicator = true;

            DayBooksService.paginate()
                .then(function (response) {
                    $scope.DayBooks = response.data;
                    $scope.showLoadingIndicator = false;
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.add = function () {
            $scope.editModalInstance = $uibModal.open({
                templateUrl: '/DayBooks/DayBooksEdit/',
                scope: $scope,
                size: 'lg',
                backdrop: 'static',
                keyboard: false
            });

            $scope.editModalInstance.result.then(function () {
                $scope.paginate();
            });
        };

        $scope.paginate();
    }]);