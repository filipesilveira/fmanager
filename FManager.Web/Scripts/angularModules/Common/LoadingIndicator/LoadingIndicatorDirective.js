angular.module('FManagerApp').directive('loadingIndicator', function () {
    return {
        restrict: 'E',
        scope: {
            busy: '='
        },
        templateUrl: '/Scripts/angularModules/Common/LoadingIndicator/LoadingIndicatorTemplate.html'
    };
});


