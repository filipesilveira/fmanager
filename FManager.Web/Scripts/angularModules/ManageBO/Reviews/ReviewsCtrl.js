angular.module('FManagerApp').controller('ReviewsCtrl', ['$scope', 'ReviewsService', 'ParitiesService', 'DaysOfWeekService', 'AccountsService', 'NotifyFactory', 'NgTableParams',
    function ($scope, ReviewsService, ParitiesService, DaysOfWeekService, AccountsService, NotifyFactory, NgTableParams) {

        $scope.ReviewsData = { Operations: true };
        $scope.DayOfWeekCombobox = DaysOfWeekService.data;
        
        $scope.initialize = function () {
            $scope.showLoadingIndicator = true;

            ParitiesService.paginate()
                .then(function (response) {
                    $scope.ParitiesCombobox = response.data;

                    AccountsService.getCombobox()
                        .then(function (response) {
                            $scope.AccountsCombobox = response.data;
                            $scope.initializeTable();
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
            $scope.reviewsTableParams = new NgTableParams(
                {
                    page: 1,
                    count: 10
                },
                {
                    getData: function (params) {
                        $scope.showLoadingIndicator = true;
                        $scope.ReviewsData.Page = params.page();
                        $scope.ReviewsData.Count = params.count();

                        return ReviewsService.paginate($scope.ReviewsData)
                            .then(function (response) {
                                params.total(response.data.Total);
                                $scope.Reviews = response.data.Results;
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