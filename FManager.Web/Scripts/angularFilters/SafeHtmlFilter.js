angular.module("FManagerApp").filter('safeHtml', function ($sce) {
    return function (val) {
        return $sce.trustAsHtml(val);
    };
});