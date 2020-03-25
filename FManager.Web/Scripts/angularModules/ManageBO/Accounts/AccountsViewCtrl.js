angular.module('FManagerApp').controller('AccountsViewCtrl', ['$scope', 'AccountsService', 'SectionsService', 'NotifyFactory', '$uibModal', '$routeParams', '$location', '$filter', 'NgTableParams', 'CsvFactory',
    function ($scope, AccountsService, SectionsService, NotifyFactory, $uibModal, $routeParams, $location, $filter, NgTableParams, CsvFactory) {

        $scope.initialize = function () {
            $scope.showLoadingIndicator = true;

            if (!$routeParams.accountId) {
                $scope.showLoadingIndicator = false;
                return;
            }

            $scope.accountId = $routeParams.accountId;

            $scope.get();
        };

        $scope.get = function () {
            AccountsService.get($scope.accountId)
                .then(function (response) {
                    $scope.AccountData = response.data;

                    if (!$scope.tableInitialized) {
                        $scope.initializeTable();
                    }
                    else {
                        $scope.sectionsTableParams.reload();
                    }
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.initializeTable = function () {
            $scope.tableInitialized = true;
            $scope.sectionsTableParams = new NgTableParams(
                {
                    page: 1,
                    count: 10
                },
                {
                    getData: function (params) {
                        $scope.showLoadingIndicator = true;
                        return SectionsService.paginate(params, $scope.accountId)
                            .then(function (response) {
                                params.total(response.data.Total);
                                $scope.sections = response.data.Results;
                                $scope.accountBalanceChart();
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

        $scope.edit = function () {
            $scope.editAccountModalInstance = $uibModal.open({
                templateUrl: '/Accounts/AccountsEdit/',
                scope: $scope,
                size: 'lg',
                backdrop: 'static',
                keyboard: false
            });

            $scope.editAccountModalInstance.result.then(function () {
                $scope.initialize();
            });
        };

        $scope.delete = function () {
            if (!$scope.accountId) {
                return;
            }

            if (!confirm("Você tem certeza? Isso não pode ser desfeito.")) {
                return;
            }

            $scope.showLoadingIndicator = true;

            AccountsService.delete($scope.accountId)
                .then(function () {
                    $location.path('/accounts');
                    $scope.showLoadingIndicator = false;
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.deleteSection = function (id) {
            if (!id) {
                return;
            }

            if (!confirm("Você tem certeza? Isso não pode ser desfeito.")) {
                return;
            }

            $scope.showLoadingIndicator = true;

            SectionsService.delete(id)
                .then(function () {
                    $scope.get();
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.accountBalanceChart = function () {

            var divAccountBalanceHistoryChart = document.getElementById("accountBalanceHistoryChart");
            divAccountBalanceHistoryChart.innerHTML = "";

            var accountBalanceHistoryChart = document.createElement("canvas");
            var chartData = accountBalanceChartGetData();
            var ctx = accountBalanceHistoryChart.getContext('2d');
            var gradientStroke = ctx.createLinearGradient(0, 230, 0, 50);

            divAccountBalanceHistoryChart.append(accountBalanceHistoryChart);

            gradientStroke.addColorStop(1, 'rgba(29,140,248,0.2)');
            gradientStroke.addColorStop(0.4, 'rgba(29,140,248,0.0)');
            gradientStroke.addColorStop(0, 'rgba(29,140,248,0)'); // blue colors

            var config = {
                type: 'line',
                data: {
                    labels: chartData.labels,
                    datasets: [{
                        label: `Valor da banca (${$scope.AccountData.CurrencyName})`,
                        fill: true,
                        backgroundColor: gradientStroke,
                        borderColor: '#1f8ef1',
                        borderWidth: 2,
                        borderDash: [],
                        borderDashOffset: 0.0,
                        pointBackgroundColor: '#1f8ef1',
                        pointBorderColor: 'rgba(255,255,255,0)',
                        pointHoverBackgroundColor: '#00d6b4',
                        pointBorderWidth: 20,
                        pointHoverRadius: 4,
                        pointHoverBorderWidth: 15,
                        pointRadius: 4,
                        data: chartData.data
                    }]
                },
                options: gradientChartOptionsConfigurationWithTooltipGreen
            };

            var myChartData = new Chart(ctx, config);
        };

        accountBalanceChartGetData = function () {
            var labels = [];
            var data = [];

            angular.forEach($scope.sections, function (section) {
                if (section.DateAndTime) {
                    labels.unshift($filter('date')(section.DateAndTime, 'dd/MM/yyyy'));
                    data.unshift((section.Balance + section.Result).toFixed(2));
                }
            });

            return {
                labels: labels,
                data: data
            };
        };

        $scope.$watch('fileContent', function (fileContent) {
            $scope.showLoadingIndicator = true;

            var json = CsvFactory.toJson(fileContent, $scope.accountId);

            if (!json) {
                $scope.showLoadingIndicator = false;
                return;
            }

            SectionsService.createIQOptionSections(json)
                .then(function () {
                    NotifyFactory.success();
                    $scope.get();
                })
                .catch(function () {
                    NotifyFactory.error();
                    $scope.showLoadingIndicator = false;
                });
        });

        $scope.initialize();
    }]);