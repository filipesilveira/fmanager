angular.module("FManagerApp").controller("AccountsCtrl", ['$scope', 'AccountsService', 'CurrenciesService', 'NotifyFactory',
    function ($scope, AccountsService, CurrenciesService, NotifyFactory) {
        $scope.AccountData = {};
        $scope.showLoadingIndicator = false;

        $scope.initialize = function () {
            $scope.showLoadingIndicator = true;

            CurrenciesService.paginate()
                .then(function (response) {
                    $scope.CurrenciesCombobox = response.data;

                    if ($scope.accountId) {
                        AccountsService.get($scope.accountId)
                            .then(function (response) {
                                $scope.AccountData = response.data;
                                $scope.showLoadingIndicator = false;
                            })
                            .catch(function () {
                                NotifyFactory.error();
                            });
                    } else {
                        $scope.showLoadingIndicator = false;
                    }
                })
                .catch(function () {
                    NotifyFactory.error();
                });
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
            if ($scope.accountId) {
                return AccountsService.put($scope.accountId, $scope.AccountData);
            }
            else {
                return AccountsService.post($scope.AccountData);
            }
        };

        $scope.delete = function () {
            if (!$scope.accountId) {
                return;
            }

            if (!confirm("Você tem certeza? Isso não pode ser desfeito.")) {
                return;
            }

            AccountsService.delete($scope.accountId)
                .then(function () {
                    $scope.closeModal();
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.closeModal = function () {
            $scope.editAccountModalInstance.close();
        };

        $scope.dismissModal = function () {
            $scope.editAccountModalInstance.dismiss();
        };

        $scope.initialize();
    }]);