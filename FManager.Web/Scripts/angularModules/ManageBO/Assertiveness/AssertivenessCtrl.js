angular.module('FManagerApp').controller('AssertivenessCtrl', ['$scope', 'AssertivenessService', 'ParitiesService', 'DaysOfWeekService', 'AccountsService', 'NotifyFactory', 'NgTableParams',
    function ($scope, AssertivenessService, ParitiesService, DaysOfWeekService, AccountsService, NotifyFactory, NgTableParams) {

        $scope.AssertivenessData = {};
        $scope.DayOfWeekCombobox = DaysOfWeekService.data;

        $scope.initialize = function () {
            $scope.showLoadingIndicator = true;

            ParitiesService.paginate()
                .then(function (response) {
                    $scope.ParitiesCombobox = response.data;

                    AccountsService.getCombobox()
                        .then(function (response) {
                            $scope.AccountsCombobox = response.data;
                            $scope.paginate();
                        })
                        .catch(function () {
                            NotifyFactory.error();
                            $scope.showLoadingIndicator = false;
                        });
                })
                .catch(function () {
                    NotifyFactory.error();
                    $scope.showLoadingIndicator = false;
                });
        };

        $scope.paginate = function () {
            $scope.showLoadingIndicator = true;

            AssertivenessService.paginate($scope.AssertivenessData)
                .then(function (response) {
                    $scope.Assertiveness = response.data;
                    $scope.assertivenessTableParams = new NgTableParams({ sorting: { ParityName: "asc" }, page: 1, count: 100 }, { dataset: response.data.Results });
                    $scope.showLoadingIndicator = false;
                })
                .catch(function () {
                    NotifyFactory.error();
                    $scope.showLoadingIndicator = false;
                });
        };

        $scope.initialize();
    }]);