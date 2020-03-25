angular.module('FManagerApp').controller('AccountsListCtrl', ['$scope', 'AccountsService', 'NotifyFactory', '$uibModal',
    function ($scope, AccountsService, NotifyFactory, $uibModal) {

        $scope.paginate = function () {
            $scope.showLoadingIndicator = true;

            AccountsService.paginate()
                .then(function (response) {
                    $scope.Accounts = response.data;
                    $scope.showLoadingIndicator = false;
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.add = function () {
            $scope.edit();
        };

        $scope.edit = function (id) {
            $scope.accountId = id;

            $scope.editAccountModalInstance = $uibModal.open({
                templateUrl: '/Accounts/AccountsEdit/',
                scope: $scope,
                size: 'lg',
                backdrop: 'static',
                keyboard: false
            });

            $scope.editAccountModalInstance.result.then(function () {
                $scope.paginate();
            });
        };

        $scope.paginate();
    }]);