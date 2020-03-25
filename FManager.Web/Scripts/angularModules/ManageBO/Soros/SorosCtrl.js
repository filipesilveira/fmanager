angular.module('FManagerApp').controller('SorosCtrl', ['$scope',
    function ($scope) {

        $scope.SorosData = { Payout: 89, Length: 10 };

        $scope.updateData = function () {
            $scope.Soros = [];

            var investment = $scope.SorosData.Investment;
            var payout = $scope.SorosData.Payout / 100;

            for (var i = 0; i < $scope.SorosData.Length; i++) {
                $scope.Soros.push({
                    Investment: investment.toFixed(2),
                    Profit: (investment * payout).toFixed(2),
                    Total: (investment + investment * payout).toFixed(2) 
                });

                investment += investment * payout;
            }
        };
    }]);