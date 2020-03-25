angular.module('FManagerApp').controller('TradingHistoryCtrl', ['$scope', 'TradingHistoryService', 'ParitiesService', 'DaysOfWeekService', 'AccountsService', 'NotifyFactory', 'NgTableParams',
    function ($scope, TradingHistoryService, ParitiesService, DaysOfWeekService, AccountsService, NotifyFactory, NgTableParams) {

        $scope.TradingHistoryData = {};
        $scope.DayOfWeekCombobox = DaysOfWeekService.data;

        $scope.initialize = function () {
            $scope.showLoadingIndicator = true;

            ParitiesService.paginate()
                .then(function (response) {
                    $scope.ParitiesCombobox = response.data;

                    AccountsService.getCombobox()
                        .then(function (response) {
                            $scope.AccountsCombobox = response.data;
                            $scope.showLoadingIndicator = false;
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

        $scope.initializeTable = function () {

            if ($scope.tradingHistoryTableParams) {
                $scope.tradingHistoryTableParams.page(1);
                $scope.tradingHistoryTableParams.reload();
                return;
            }

            $scope.tradingHistoryTableParams = new NgTableParams(
                {
                    page: 1,
                    count: 10
                },
                {
                    getData: function (params) {
                        $scope.showLoadingIndicator = true;
                        $scope.TradingHistoryData.Page = params.page();
                        $scope.TradingHistoryData.Count = params.count();

                        return TradingHistoryService.paginate($scope.TradingHistoryData)
                            .then(function (response) {
                                params.total(response.data.Total);
                                $scope.TradingHistory = response.data;
                                $scope.showLoadingIndicator = false;
                                $('.ps').scrollTop(0);
                                return response.data.Results;
                            })
                            .catch(function () {
                                NotifyFactory.error();
                            });
                    }
                });
        };

        $scope.initialize();
    }]);