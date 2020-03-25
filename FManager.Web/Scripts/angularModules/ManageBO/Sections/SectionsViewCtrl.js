angular.module('FManagerApp').controller('SectionsViewCtrl', ['$scope', 'SectionsService', 'EntriesService', '$uibModal', '$routeParams', '$location', 'NotifyFactory', 'CsvFactory', '$filter',
    function ($scope, SectionsService, EntriesService, $uibModal, $routeParams, $location, NotifyFactory, CsvFactory, $filter) {

        $scope.initialize = function () {
            $scope.accountId = $routeParams.accountId;
            $scope.sectionId = $routeParams.sectionId !== 'new' ? $routeParams.sectionId : null;
            $scope.get();
        };

        $scope.get = function () {
            $scope.showLoadingIndicator = true;

            SectionsService.get($scope.sectionId, $scope.accountId)
                .then(function (response) {
                    $scope.SectionData = response.data;
                    $scope.sectionId = $scope.SectionData.SectionId;
                    $scope.paginate();
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.save = function () {
            SectionsService.put($scope.sectionId, $scope.SectionData)
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.delete = function () {
            if (!$scope.sectionId) {
                return;
            }

            if (!confirm("Are you sure? This can't be undone")) {
                return;
            }

            $scope.showLoadingIndicator = true;

            SectionsService.delete($scope.sectionId)
                .then(function () {
                    $location.path(`/accounts/${$scope.accountId}`);
                    $scope.showLoadingIndicator = false;
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.paginate = function () {
            $scope.showLoadingIndicator = true;

            EntriesService.paginate($scope.sectionId)
                .then(function (response) {
                    $scope.entries = response.data;
                    $scope.initializeCharts();
                    $scope.showLoadingIndicator = false;
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.addEntry = function () {
            $scope.editEntry();
        };

        $scope.editEntry = function (id) {
            $scope.entryId = id;

            $scope.editEntryModalInstance = $uibModal.open({
                templateUrl: '/Entries/EntriesEdit/',
                scope: $scope,
                size: 'lg',
                backdrop: 'static',
                keyboard: false
            });

            $scope.editEntryModalInstance.result.then(function () {
                $scope.get();
                $scope.entryId = null;
            });
        };

        $scope.deleteEntry = function (id) {
            if (!id) {
                return;
            }

            if (!confirm("Você tem certeza? Isso não pode ser desfeito.")) {
                return;
            }

            $scope.showLoadingIndicator = true;

            EntriesService.delete(id)
                .then(function () {
                    $scope.get();
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.initializeCharts = function () {

            if (!$scope.entries || $scope.entries.length === 0) {
                return;
            }

            winAndLossSetData();

            sectionResultChart();
            winAndLossChart();
            entriesChart();
        };

        var sectionResultChart = function () {

            var divSectionResultChart = document.getElementById("sectionResultChart");
            divSectionResultChart.innerHTML = "";

            var sectionResultChart = document.createElement("canvas");
            var chartData = sectionResultChartGetData();
            var ctxGreen = sectionResultChart.getContext("2d");
            var gradientStroke = ctxGreen.createLinearGradient(0, 230, 0, 50);

            divSectionResultChart.append(sectionResultChart);

            gradientStroke.addColorStop(1, 'rgba(29,140,248,0.2)');
            gradientStroke.addColorStop(0.4, 'rgba(29,140,248,0.0)');
            gradientStroke.addColorStop(0, 'rgba(29,140,248,0)'); // blue colors

            var data = {
                labels: chartData.labels,
                datasets: [{
                    label: `Saldo (${$scope.SectionData.CurrencyName})`,
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
                    data: chartData.data,
                }]
            };

            var myChart = new Chart(ctxGreen, {
                type: 'line',
                data: data,
                options: gradientChartOptionsConfigurationWithTooltipGreen
            });
        };

        var winAndLossChart = function () {

            var divWinAndLossChart = document.getElementById("winAndLossChart");
            divWinAndLossChart.innerHTML = "";

            var winAndLossChart = document.createElement("canvas");
            var ctx = winAndLossChart.getContext("2d");
            var gradientStroke = ctx.createLinearGradient(0, 230, 0, 50);

            divWinAndLossChart.append(winAndLossChart);

            gradientStroke.addColorStop(1, 'rgba(29,140,248,0.2)');
            gradientStroke.addColorStop(0.4, 'rgba(29,140,248,0.0)');
            gradientStroke.addColorStop(0, 'rgba(29,140,248,0)'); // blue colors

            var myChart = new Chart(ctx, {
                type: 'bar',
                responsive: true,
                legend: {
                    display: false
                },
                data: {
                    labels: ['WIN', 'LOSS'],
                    datasets: [{
                        label: "Total",
                        fill: true,
                        backgroundColor: gradientStroke,
                        hoverBackgroundColor: gradientStroke,
                        borderColor: '#1f8ef1',
                        borderWidth: 2,
                        borderDash: [],
                        borderDashOffset: 0.0,
                        data: [$scope.totalWin, $scope.totalLoss]
                    }]
                },
                options: gradientBarChartConfiguration
            });
        };

        var entriesChart = function () {

            var divEntriesChart = document.getElementById("entriesChart");
            divEntriesChart.innerHTML = "";

            var entriesChart = document.createElement("canvas");
            var chartData = entriesChartGetData();
            var ctxBlue = entriesChart.getContext("2d");
            var gradientStroke = ctxBlue.createLinearGradient(0, 230, 0, 50);

            divEntriesChart.append(entriesChart);

            gradientStroke.addColorStop(1, 'rgba(29,140,248,0.2)');
            gradientStroke.addColorStop(0.4, 'rgba(29,140,248,0.0)');
            gradientStroke.addColorStop(0, 'rgba(29,140,248,0)'); // blue colors

            var data = {
                labels: chartData.labels,
                datasets: [{
                    label: `Valor (${$scope.SectionData.CurrencyName})`,
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
            };

            var myChart = new Chart(ctxBlue, {
                type: 'line',
                data: data,
                options: gradientChartOptionsConfigurationWithTooltipGreen

            });
        };

        var winAndLossSetData = function () {
            $scope.totalWin = $scope.entries.filter(w => w.Win).length;
            $scope.totalLoss = $scope.entries.filter(w => !w.Win).length;
        };

        var sectionResultChartGetData = function () {
            var labels = [];
            var data = [];

            angular.forEach($scope.entries, function (entry) {
                labels.unshift($filter('date')(entry.DateAndTime, 'HH:mm'));
                data.unshift(entry.Balance.toFixed(2));
            });

            return {
                labels: labels,
                data: data
            };
        };

        var entriesChartGetData = function () {
            var labels = [];
            var data = [];

            angular.forEach($scope.entries, function (entry) {
                labels.unshift($filter('date')(entry.DateAndTime, 'HH:mm'));
                data.unshift(entry.Value);
            });

            return {
                labels: labels,
                data: data
            };
        };

        $scope.$watch('fileContent', function (fileContent) {
            $scope.showLoadingIndicator = true;

            var json = CsvFactory.toJson(fileContent, $scope.sectionId);

            if (!json) {
                $scope.showLoadingIndicator = false;
                return;
            }

            EntriesService.createIQOptionEntries(json)
                .then(function () {
                    NotifyFactory.success();
                    $scope.get();
                    $scope.showLoadingIndicator = false;
                })
                .catch(function () {
                    NotifyFactory.error();
                    $scope.showLoadingIndicator = false;
                });
        });

        $scope.initialize();
    }]);