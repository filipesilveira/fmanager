angular.module("FManagerApp").controller("EntriesCtrl", ['$scope', 'EntriesService', 'SectionsService', 'ParitiesService', 'NotifyFactory',
    function ($scope, EntriesService, SectionsService, ParitiesService, NotifyFactory) {
        $scope.EntryData = { SectionId: $scope.sectionId };
        $scope.showLoadingIndicator = false;
        $scope.saveAndNext = !$scope.entryId;

        $scope.initialize = function () {
            $scope.showLoadingIndicator = true;

            ParitiesService.paginate()
                .then(function (response) {
                    $scope.ParitiesCombobox = response.data;

                    if ($scope.entryId) {
                        EntriesService.get($scope.entryId)
                            .then(function (response) {
                                $scope.EntryData = response.data;
                                $scope.showLoadingIndicator = false;
                            })
                            .catch(function () {
                                NotifyFactory.error();
                            });
                    }
                    else {
                        $scope.showLoadingIndicator = false;
                    }
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.save = function (win) {
            $scope.showLoadingIndicator = true;
            $scope.EntryData.Win = win;
            $scope.getSavePromise()
                .then(() => {
                    NotifyFactory.success();
                    if ($scope.saveAndNext) {
                        $scope.EntryData = {
                            SectionId: $scope.sectionId,
                            Payout: $scope.EntryData.Payout,
                            ParityId: $scope.EntryData.ParityId
                        };
                        $scope.getSection();
                    } else {
                        $scope.closeModal();
                    }
                    $scope.showLoadingIndicator = false;
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.getSavePromise = function () {
            if ($scope.entryId) {
                return EntriesService.put($scope.entryId, $scope.EntryData);
            }
            else {
                return EntriesService.post($scope.EntryData);
            }
        };

        $scope.delete = function () {
            if (!$scope.entryId) {
                return;
            }

            if (!confirm("Você tem certeza? Isso não pode ser desfeito.")) {
                return;
            }

            EntriesService.delete($scope.entryId)
                .then(function () {
                    $scope.closeModal();
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.closeModal = function () {
            $scope.editEntryModalInstance.close();
        };

        $scope.getSection = function () {
            SectionsService.get($scope.sectionId, $scope.accountId)
                .then(function (response) {
                    $scope.SectionData = response.data;
                })
                .catch(function () {
                    NotifyFactory.error();
                });
        };

        $scope.getAccountPercentage = function (percentage) {
            if (!percentage || !$scope.SectionData.AccountBalance) {
                return;
            }

            var total = $scope.SectionData.AccountBalance * percentage / 100;

            return total.toFixed(2);
        };

        $scope.initialize();
    }]);