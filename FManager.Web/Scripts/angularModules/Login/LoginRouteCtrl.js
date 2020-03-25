angular.module('FManagerApp').controller('LoginRouteCtrl', ['$scope', '$rootScope',
    function ($scope, $rootScope) {

        $rootScope.$on('$routeChangeStart', function () {
            $scope.showLoadingIndicator = true;
        });

        $rootScope.$on('$routeChangeSuccess', function () {
            $scope.showLoadingIndicator = false;
        });
    }]);